import { Component, inject } from '@angular/core';
import { AcademicYearService } from '../../core/services/academic-year.service';
import { AsyncPipe } from '@angular/common';
import {
  FormsModule,
  ReactiveFormsModule,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import {
  MatDatepickerModule,
  MatDateSelectionModel,
} from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { Observable } from 'rxjs';
import { AcademicYear } from '../../core/models/academicYear.interface';
import { provideNativeDateAdapter } from '@angular/material/core';

@Component({
  selector: 'app-academic-year',
  imports: [
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe,
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './academic-year.component.html',
  styleUrl: './academic-year.component.scss',
})
export class AcademicYearComponent {
  private academicYearService = inject(AcademicYearService);

  public academicYear$: Observable<AcademicYear> =
    this.academicYearService.academicYear$;

  public editMode = false;
  public academicYearForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.academicYearForm = this.fb.group({
      id: ['', Validators.required],
      dateStart: ['', Validators.required],
      dateEnd: ['', Validators.required],
    });
  }

  edit() {
    this.editMode = !this.editMode;
    if (this.editMode) {
      this.academicYear$.subscribe((academicYear) => {
        this.academicYearForm.patchValue({
          id: academicYear.id,
          dateStart: academicYear.dateStart,
          dateEnd: academicYear.dateEnd,
        });
      });
    }
  }

  cancel() {
    this.editMode = !this.editMode;
    console.log(this.academicYearForm.value);
  }

  save() {
    this.editMode = !this.editMode;
    if (this.academicYearForm.valid) {
      this.academicYearService
        .updateAcademicYear(this.academicYearForm.value)
        .subscribe();
    }
  }
}
