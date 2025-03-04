import { Component, inject } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { AcademicYearService } from '../../../core/services/academic-year.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-academic-year-add-form',
  imports: [
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatDatepickerModule,
    CommonModule,
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './academic-year-add-form.component.html',
  styleUrl: './academic-year-add-form.component.scss',
})
export class AcademicYearAddFormComponent {
  private readonly router = inject(Router);
  private academicYearService = inject(AcademicYearService);
  public academicYearForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.academicYearForm = this.fb.group({
      code: ['', Validators.required],
      dateStart: ['', Validators.required],
      dateEnd: ['', Validators.required],
    });
  }

  cancel() {
    // redirect to academicYear
    this.router.navigate(['/academicYear']);
  }

  save() {
    this.academicYearService
      .addAcademicYear(this.academicYearForm.value)
      .subscribe();
    // redirect to academicYear
    this.router.navigate(['/academicYear']);
  }
}
