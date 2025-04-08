import { Component, inject, input, OnInit, output, SimpleChanges } from '@angular/core';
import { Option } from '../../../core/models/option.interface';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-course-form-custom-input',
  imports: [ReactiveFormsModule,
            FormsModule,
            MatFormFieldModule,
            MatIconModule,
            RouterModule,
            MatButtonModule,
            CommonModule],
  templateUrl: './course-form-custom-input.component.html',
  styleUrl: './course-form-custom-input.component.scss'
})
export class CourseFormCustomInputComponent implements OnInit{
  optionList = input<Option[]>();
  optionIdAndGradeInput = input<{optionId: string,grade: number}>();
  optionIdAndGrade = output<{optionId: string,grade: number}>();
  optionIdDelete = output<string>();
  isValid = output<boolean>();
  courseForm: FormGroup;
  formBuilder = inject(FormBuilder);


  constructor(){
    this.courseForm = this.formBuilder.group({
        optionId:['',Validators.required],
        grade:[1,Validators.required],
    });
  }

  ngOnInit(): void {
    this.isValid.emit(false);
  }
  


  ngOnChanges(changes: SimpleChanges){
      this.courseForm.patchValue({
        optionId: this.optionIdAndGradeInput()?.optionId,
        grade: this.optionIdAndGradeInput()?.grade,
      })
    this.courseForm.controls['optionId'].markAsTouched();
    this.courseForm.controls['grade'].markAsTouched();
    
  }

  onGradeChanged(event:any){
    const selectedOptionId = this.courseForm.get('optionId')?.value;
    if(selectedOptionId){
      this.optionIdAndGrade.emit({optionId :selectedOptionId, grade :event.target.value});
      this.isValid.emit(true);
    }
  }

  onDeleteOption(optionId: string){
    this.optionIdDelete.emit(optionId);
  }

  get selectedOptionId(){
    return this.courseForm.get('optionId')?.value;
  }




}
