import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CourseService } from '../../../core/services/course.service';
import { Course } from '../../../core/models/course.interface';

@Component({
  selector: 'app-course-edit-form',
  imports: [ReactiveFormsModule,
            FormsModule,
            MatFormFieldModule,
            MatIconModule,
            RouterModule,
            MatButtonModule,
            CommonModule],
  templateUrl: './course-edit-form.component.html',
  styleUrl: './course-edit-form.component.scss'
})
export class CourseEditFormComponent implements OnInit{
  private readonly courseService = inject(CourseService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  courseForm: FormGroup;
  course: Course | null = null;
  formBuilder = inject(FormBuilder);

  constructor(){
      this.courseForm = this.formBuilder.group({
        name:['',Validators.required],
        code:['',Validators.required],
        description:['',Validators.required],
        totalHours:['',Validators.required],
        weeklyHours:['',Validators.required],
        semester:['',Validators.required],
        credits:['',Validators.required],
        equipmentType:[null,null]//TODO Changer quand on a equipmentType service(Faire un Dialog pour ajouter quantité et equipment)
      });
    }

  ngOnInit(): void {
    //TODO : Fetch tous les equipment type dispo
    this.route.paramMap.subscribe(params => {
      this.courseService.getCourseById(params.get('id')!).subscribe({
        next: (e) => {
          this.course = e;
          this.courseForm.patchValue(this.course);
        },
        error: (e) => alert("Problem while editing the course")
      })
    })
  }

  onSave(){
      const course: Course = {
        ...this.courseForm.value,
        equipmentType: null,
        id: this.course?.id
      }
      this.courseService.updateCourse(course).subscribe({
        next: (v) => console.log(v),
        error: (e) => {
          const errorMessages = Object.entries(e.error.errors)
          .map(([subject,messages]) =>`${subject}: ${messages}`)
          .join('\n');
          alert(errorMessages);
        },
        complete: () => this.router.navigate(['/courses'])
      })
    }

  onCancel(){
    this.router.navigate(['/courses']);
  }
  
}
