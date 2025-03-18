import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CourseService } from '../../../core/services/course.service';
import { Course } from '../../../core/models/course.interface';
import { EquipmentService } from '../../../core/services/equipment.service';
import { EquipmentType } from '../../../core/models/equipment.interface';

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
  private readonly equipmentTypeService = inject(EquipmentService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  courseForm: FormGroup;
  course: Course | null = null;
  formBuilder = inject(FormBuilder);
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;

  constructor(){
      this.courseForm = this.formBuilder.group({
        name:['',Validators.required],
        code:['',Validators.required],
        description:['',Validators.required],
        totalHours:['',Validators.required],
        weeklyHours:['',Validators.required],
        semester:['',Validators.required],
        credits:['',Validators.required],
        equipmentType: this.formBuilder.array([], null),
      });
    }

  ngOnInit(): void {
    this.equipmentTypeService.getEquipmentTypes().subscribe();
    this.route.paramMap.subscribe(params => {
      this.courseService.getCourseById(params.get('id')!).subscribe({
        next: (e) => {
          this.course = e;
          this.courseForm.patchValue(this.course);
          this.course.equipmentType?.forEach((equipmentType) => this.equipmentTypeArray.push(this.formBuilder.control(equipmentType)));
        },
        error: (e) => alert("Problem while editing the course")
      })
    })
  }

  get equipmentTypeArray() {
      return this.courseForm.get('equipmentType') as FormArray;
    }

  get selectedEquipmentType(): string[]{
    return this.equipmentTypeArray.value.map((arr: { id: string; }) => arr.id);
  }

  onSave(){
      const course: Course = {
        ...this.courseForm.value,
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
        complete: () => this.goBack()
      })
    }

  onCancel(){
    this.goBack();
  }

  selectEquipment(equipmentType: EquipmentType, event: any){
      const checked = event.target.checked;
      const indexEquipmentType = this.selectedEquipmentType.indexOf(equipmentType.id!);
      if (checked) {   
        this.equipmentTypeArray.push(this.formBuilder.control(equipmentType.id));
      } else {
        this.equipmentTypeArray.removeAt(indexEquipmentType);
      }
      this.courseForm.controls['equipmentType'].markAsTouched();
    }
    
    goBack(){
      const origin = this.route.snapshot.queryParamMap.get('from');
      if(origin === 'details') this.router.navigate(['/courses/view',this.course?.id]);
      else this.router.navigate(['/courses']);
    }
  
}
