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
import { Option } from '../../../core/models/option.interface';
import { OptionService } from '../../../core/services/option.service';
import { CourseFormCustomInputComponent } from "../course-form-custom-input/course-form-custom-input.component";

@Component({
  selector: 'app-course-add-form',
  imports: [ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
    MatButtonModule,
    CommonModule, CourseFormCustomInputComponent],
  templateUrl: './course-add-form.component.html',
  styleUrl: './course-add-form.component.scss'
})
export class CourseAddFormComponent implements OnInit{
  private readonly courseService = inject(CourseService);
  private readonly equipmentTypeService = inject(EquipmentService);
  private readonly optionService = inject(OptionService);
  private readonly router = inject(Router);
  courseForm: FormGroup;
  formBuilder = inject(FormBuilder);
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;
  optionList$ = this.optionService.options$;
  courseOptions:string[] = [];
  counter: number = 1;
  selectedOptionId: string | undefined;
  formValid: boolean = true;

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
      optionGrades: this.formBuilder.group({}),
    });
  }

  ngOnInit(): void {
    this.equipmentTypeService.getEquipmentTypes().subscribe();
    this.optionService.getOptions();
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

  get optionGradesFormGroup() {
    return this.courseForm.get('optionGrades') as FormGroup;
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
  

  onOptionSelected(event: { optionId: string; grade: number },index: number){
    if (event.optionId) {
  
     const previousOptionId = this.courseOptions[index];
  
      if (previousOptionId && previousOptionId !== event.optionId) {
        this.optionGradesFormGroup.removeControl(previousOptionId);
      }
  
      if (!this.optionGradesFormGroup.get(event.optionId)) {
        this.optionGradesFormGroup.addControl(event.optionId, this.formBuilder.control(event.grade));
      } else {
        this.optionGradesFormGroup.get(event.optionId)?.setValue(event.grade);
      }
  
      this.courseOptions[index] = event.optionId;

    }
  }

  onDeleteOption(optionId: string){
    const indexToDelete = this.courseOptions.indexOf(optionId);
    if (indexToDelete !== -1) {
      this.optionGradesFormGroup.removeControl(optionId);
      this.courseOptions.splice(indexToDelete, 1);
      this.counter--;
    }
  }

  addOption(){
    this.courseOptions?.push('');
    console.log(this.courseOptions);
  }

  getGradeFromOptionId(id: string): number{
      return this.optionGradesFormGroup.get(id)?.value;
    }

}
