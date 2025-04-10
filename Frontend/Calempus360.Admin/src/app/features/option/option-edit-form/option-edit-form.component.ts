import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators, AbstractControl, FormArray } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { OptionService } from '../../../core/services/option.service';
import { Option } from '../../../core/models/option.interface';
import { CourseService } from '../../../core/services/course.service';
import { Course } from '../../../core/models/course.interface';

@Component({
  selector: 'app-option-edit-form',
  imports: [ReactiveFormsModule,
          FormsModule,
          MatFormFieldModule,
          MatIconModule,
          RouterModule,
          MatButtonModule,
          CommonModule,],
  templateUrl: './option-edit-form.component.html',
  styleUrl: './option-edit-form.component.scss'
})
export class OptionEditFormComponent implements OnInit{
  private readonly courseService = inject(CourseService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  optionForm: FormGroup;
  formBuilder = inject(FormBuilder);
  private readonly optionService = inject(OptionService)
  option: Option | null = null;
  courseList$ = this.courseService.courses$;
    constructor () {
        this.optionForm = this.formBuilder.group({
            name: ['',Validators.required],
            code: ['',Validators.required],
            description: ['',Validators.required],
        });
      }

  ngOnInit(): void {
    this.courseService.getCourses();
    this.route.paramMap.subscribe(params => {
      this.optionService.getOptionById(params.get('id')!).subscribe({
        next: (e) => {
          this.option = e;
          this.optionForm.patchValue(this.option);
        },
        error: (e) => alert("Problem while editing the option")
      })
    })
  }


  onSave(){
    const option: Option = {
      ...this.optionForm.value,
      id: this.option?.id
    }
    this.optionService.updateOption(option).subscribe({
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

  goBack(){
    const origin = this.route.snapshot.queryParamMap.get('from');
    if(origin === 'details') this.router.navigate(['/options/view',this.option?.id]);
    else if(origin === 'courseView') this.router.navigate(['/courses/view',this.route.snapshot.queryParamMap.get('courseId')]);
    else this.router.navigate(['/options']);
  }

}
