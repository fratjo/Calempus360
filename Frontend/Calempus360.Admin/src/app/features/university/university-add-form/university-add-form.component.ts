import { Component, inject } from '@angular/core';
import { UniversityService } from '../../../core/services/university.service';
import { Observable } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { University } from '../../../core/models/university.interface';
import { MatFormField, MatInput } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-university-add-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatInput,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
  ],
  templateUrl: './university-add-form.component.html',
  styleUrl: './university-add-form.component.scss',
})
export class UniversityAddFormComponent {
  private readonly router = inject(Router);
  private universityService = inject(UniversityService);
  public universityForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.universityForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      code: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }

  ngOnInit() {
    const universityData = this.universityService.getUniversityByName('');
  }

  cancel() {
    // redirect to university
    this.router.navigate(['/university']);
  }

  save() {
    this.universityService.addUniversity(this.universityForm.value);
    // redirect to university
    this.router.navigate(['/university']);
  }
}
