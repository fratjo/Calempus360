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
import { OptionService } from '../../../core/services/option.service';
import { CourseFormCustomInputComponent } from '../course-form-custom-input/course-form-custom-input.component';

@Component({
  selector: 'app-course-edit-form',
  imports: [ReactiveFormsModule,
            FormsModule,
            MatFormFieldModule,
            MatIconModule,
            RouterModule,
            MatButtonModule,
            CommonModule,CourseFormCustomInputComponent],
  templateUrl: './course-edit-form.component.html',
  styleUrl: './course-edit-form.component.scss'
})
export class CourseEditFormComponent implements OnInit{
  private readonly courseService = inject(CourseService);
  private readonly equipmentTypeService = inject(EquipmentService);
  private readonly optionService = inject(OptionService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  courseForm: FormGroup;
  course: Course | null = null;
  formBuilder = inject(FormBuilder);
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;
  optionList$ = this.optionService.options$;
  counter: number = 1;
  courseOptions:string[] = [];

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
    this.route.paramMap.subscribe(params => {
      this.courseService.getCourseById(params.get('id')!).subscribe({
        next: (e) => {
          this.course = e;
          this.courseForm.patchValue(
            {
              name: e.name,
              code: e.code,
              description: e.description,
              totalHours: e.totalHours,
              weeklyHours: e.weeklyHours,
              semester: e.semester,
              credits: e.credits,
            }
          );
          this.course.equipmentType?.forEach((equipmentType) => this.equipmentTypeArray.push(this.formBuilder.control(equipmentType.id)));
          Object.entries(e.optionGrades!).forEach(([optionId, grade]) => {
            this.optionGradesFormGroup.addControl(optionId, this.formBuilder.control(grade, Validators.required));
            this.courseOptions.push(optionId);
          });         
        },
        error: (e) => alert("Problem while editing the course")
      })
    })
  }

  get optionGradesFormGroup() {
    return this.courseForm.get('optionGrades') as FormGroup;
  }

  get equipmentTypeArray() {
      return this.courseForm.get('equipmentType') as FormArray;
    }

  get selectedEquipmentType(): string[]{
    return this.equipmentTypeArray.value;
  }

  onSave(){
      const course: Course = {
        ...this.courseForm.value,
        id: this.course?.id
      }
      console.log(course);
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

    onOptionSelected(event: { optionId: string; grade: number },index: number){
      if (event.optionId) {
        console.log(`Option sélectionnée: ${event.optionId} avec grade: ${event.grade} à l'index ${index}`);
    
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

    goBack(){
      const origin = this.route.snapshot.queryParamMap.get('from');
      if(origin === 'details') this.router.navigate(['/courses/view',this.course?.id]);
      else if(origin === 'optionView') this.router.navigate(['/options/view',this.route.snapshot.queryParamMap.get('optionId')]);
      else this.router.navigate(['/courses']);
    }

    addOption(){
      this.courseOptions?.push('');
    }

    getGradeFromOptionId(id: string): number{
      return this.optionGradesFormGroup.get(id)?.value;
    }
  
}
