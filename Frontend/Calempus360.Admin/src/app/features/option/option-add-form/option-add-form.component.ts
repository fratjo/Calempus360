import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators, FormArray, AbstractControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterModule } from '@angular/router';
import { OptionService } from '../../../core/services/option.service';
import { CourseService } from '../../../core/services/course.service';
import { Course } from '../../../core/models/course.interface';

@Component({
  selector: 'app-option-add-form',
  imports: [ReactiveFormsModule,
        FormsModule,
        MatFormFieldModule,
        MatIconModule,
        RouterModule,
        MatButtonModule,
        CommonModule],
  templateUrl: './option-add-form.component.html',
  styleUrl: './option-add-form.component.scss'
})
export class OptionAddFormComponent implements OnInit{

  private readonly courseService = inject(CourseService);
  private readonly router = inject(Router);
  optionForm: FormGroup;
  formBuilder = inject(FormBuilder);
  private readonly optionService = inject(OptionService)

  courseList$ = this.courseService.courses$;

  constructor () {
      this.optionForm = this.formBuilder.group({
          name: ['',Validators.required],
          code: ['',Validators.required],
          description: ['',Validators.required],
          courses: this.formBuilder.array([], this.coursesSelectedCheckboxValidator()),
      });
    }


  ngOnInit(): void {
    this.courseService.getCourses();
  }

  onSave(){
    const option = this.optionForm.value;
    this.optionService.addOption(option).subscribe({
      next: (v) => console.log(v),
      error: (e) => {
        const errorMessages = Object.entries(e.error.errors)
        .map(([subject,messages]) =>`${subject}: ${messages}`)
        .join('\n');
        alert(errorMessages);
      },
      complete: () => this.router.navigate(['/options'])
    })
  }

  onCancel(){
    this.router.navigate(['/options']);
  }

  get coursesArray() {
    return this.optionForm.get('courses') as FormArray;
  }

  get selectedCourses(): string[]{
    return this.coursesArray.value.map((arr: { id: string; }) => arr.id);
  }

  selectCourse(course: Course, event: Event){
    const checked = (event.target as HTMLInputElement).checked;
    const indexCourse = this.selectedCourses.indexOf(course.id!);
    if (checked) {   
      this.coursesArray.push(this.formBuilder.control(course));
    } else {
      this.coursesArray.removeAt(indexCourse);
    }
    this.optionForm.controls['courses'].markAsTouched();
  }


  coursesSelectedCheckboxValidator(){
    return (control: AbstractControl) => {
      const formArray = control as FormArray;
      return formArray.length >= 1 ? null : { required: true };
    };
  }
}
