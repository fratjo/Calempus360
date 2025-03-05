import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormsModule,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { AcademicYearService } from '../../../core/services/academic-year.service';
import { catchError, first, of } from 'rxjs';

@Component({
  selector: 'app-academic-year-edit-form',
  imports: [
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatDatepickerModule,
    CommonModule,
  ],
  templateUrl: './academic-year-edit-form.component.html',
  styleUrl: './academic-year-edit-form.component.scss',
})
export class AcademicYearEditFormComponent implements OnInit {
  private readonly activeRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private academicYearService = inject(AcademicYearService);
  public academicYear$ = this.academicYearService.academicYear$;
  public academicYearForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.academicYearForm = this.fb.group({
      id: ['', Validators.required],
      code: ['', Validators.required],
      dateStart: ['', Validators.required],
      dateEnd: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.activeRoute.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.academicYearService
          .getAdademicYearById(id)
          .pipe(first((x) => !!x))
          .subscribe((a) => this.academicYearForm.patchValue(a));
      }
    });
  }

  cancel() {
    // redirect to academicYear
    this.router.navigate(['academic-year/change']);
  }

  save() {
    this.academicYearService
      .updateAcademicYear(this.academicYearForm.value)
      .pipe(
        catchError((err) => {
          console.error(err);
          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['academic-year/change']);
      });
    this.router.navigate(['academic-year/change']);
  }
}
