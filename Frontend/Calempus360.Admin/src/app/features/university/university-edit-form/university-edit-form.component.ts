import { Component, inject, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { catchError, first, of } from 'rxjs';
import { UniversityService } from '../../../core/services/university.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { University } from '../../../core/models/university.interface';

@Component({
  selector: 'app-university-edit-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
    MatButtonModule,
    CommonModule,
  ],
  templateUrl: './university-edit-form.component.html',
  styleUrl: './university-edit-form.component.scss',
})
export class UniversityEditFormComponent implements OnInit {
  private readonly activeRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private universityService = inject(UniversityService);
  public university$ = this.universityService.university$;
  public universityForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.universityForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      address: ['', Validators.required],
      code: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.activeRoute.paramMap.subscribe((params) => {
      const universityId = params.get('id');
      if (universityId) {
        this.universityService
          .getUniversityById(universityId)
          .pipe(first((x) => !!x))
          .subscribe((u) => this.universityForm.patchValue(u));
      }
    });
  }

  cancel() {
    // redirect to university
    this.router.navigate(['/university/change']);
  }

  save() {
    this.universityService
      .updateUniversity(this.universityForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error editing university', error);
          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['/university/change']);
      });
    this.router.navigate(['/university/change']);
  }
}
