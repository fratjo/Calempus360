import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators, FormArray, AbstractControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CourseService } from '../../../core/services/course.service';
import { Course } from '../../../core/models/course.interface';
import { EquipmentService } from '../../../core/services/equipment.service';
import { EquipmentType } from '../../../core/models/equipment.interface';

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
  private readonly equipmentTypeService = inject(EquipmentService);
  private readonly router = inject(Router);
  courseForm: FormGroup;
  formBuilder = inject(FormBuilder);
  equipmentTypeAdded: boolean = false;
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
      equipmentType: this.formBuilder.array<string>([], null),
    });
  }
  ngOnInit(): void {
    this.equipmentTypeService.getEquipmentTypes().subscribe();
  }

  onSave(){
    const course = this.courseForm.value;
    console.log(course);
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

  get equipmentTypeArray() {
    return this.courseForm.get('equipmentType') as FormArray;
  }

  get selectedEquipmentType(): string[]{
    return this.equipmentTypeArray.value.map((arr: { id: string; }) => arr.id);
  }

  onAddEquipmentType(){
    if(this.equipmentTypeList$.value.length <= 0) 
    this.equipmentTypeAdded = true;
  }

  onDeleteEquipmentType(){
    this.equipmentTypeAdded = false;
    this.equipmentTypeArray.clear();
  }

  onCancel(){
    this.router.navigate(['/courses']);
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
}
