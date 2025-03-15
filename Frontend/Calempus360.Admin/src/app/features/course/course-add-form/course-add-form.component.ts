import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators, FormArray, AbstractControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CourseService } from '../../../core/services/course.service';
import { Course } from '../../../core/models/course.interface';

@Component({
  selector: 'app-course-add-form',
  imports: [ReactiveFormsModule,
          FormsModule,
          MatFormFieldModule,
          MatIconModule,
          RouterModule,
          MatButtonModule,
          CommonModule],
  templateUrl: './course-add-form.component.html',
  styleUrl: './course-add-form.component.scss'
})
export class CourseAddFormComponent implements OnInit{
  private readonly courseService = inject(CourseService);
  private readonly router = inject(Router);
  courseForm: FormGroup;
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
  }

  onSave(){
    const course = this.courseForm.value;
    this.courseService.addCourse(course).subscribe({
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
