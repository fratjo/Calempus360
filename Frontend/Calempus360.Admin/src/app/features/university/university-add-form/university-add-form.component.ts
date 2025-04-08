import { Component, inject } from '@angular/core';
import { UniversityService } from '../../../core/services/university.service';
import { catchError, of } from 'rxjs';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-university-add-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
    MatButtonModule,
    CommonModule,
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

  cancel() {
    // redirect to university
    this.router.navigate(['/university']);
  }

  save() {
    this.universityService
      .addUniversity(this.universityForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error adding university', error);

          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['/university']);
      });
    this.router.navigate(['/university']);
  }
}
