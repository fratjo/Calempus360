import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { OptionService } from '../../../core/services/option.service';
import { Option } from '../../../core/models/option.interface';

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
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  optionForm: FormGroup;
  formBuilder = inject(FormBuilder);
  private readonly optionService = inject(OptionService)
  option: Option | null = null;
  
    constructor () {
        this.optionForm = this.formBuilder.group({
            name: ['',Validators.required],
            code: ['',Validators.required],
            description: ['',Validators.required],
            courses: [null,Validators.required],
        });
      }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.optionService.getOptionById(params.get('id')!).subscribe({
        next: (e) => {
          this.option = e;
          this.optionForm.patchValue(this.option);
        },
        error: (e) => alert("Problem while editing the option")
      })
    })
    //TODO Init liste de cours
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
      complete: () => this.router.navigate(['/options'])
    })
  }

  onCancel(){
    this.router.navigate(['/options']);
  }

}
