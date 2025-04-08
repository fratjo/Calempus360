import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  ReactiveFormsModule,
  FormsModule,
  FormGroup,
  FormBuilder,
  Validators,
  FormArray,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule, Router } from '@angular/router';
import { catchError, of } from 'rxjs';
import { UniversityService } from '../../../core/services/university.service';
import { SiteService } from '../../../core/services/site.service';

@Component({
  selector: 'app-site-add-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
    MatButtonModule,
    CommonModule,
  ],
  templateUrl: './site-add-form.component.html',
  styleUrl: './site-add-form.component.scss',
})
export class SiteAddFormComponent {
  private readonly router = inject(Router);
  private universityService = inject(UniversityService);
  private siteService = inject(SiteService);
  public siteForm: FormGroup;

  public days = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];

  constructor(public fb: FormBuilder) {
    this.siteForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      code: ['', Validators.required],
      phone: ['', Validators.required],
      schedules: this.fb.array([], Validators.required),
    });

    this.days.forEach((day, i) => {
      this.scheduleArray.push(
        this.fb.group({
          dayOfWeek: [i + 1, null],
          timeStart: ['', Validators.required],
          timeEnd: ['', Validators.required],
        }),
      );
    });
  }

  cancel() {
    // redirect to university
    this.router.navigate(['/sites']);
  }

  get scheduleArray() {
    return this.siteForm.get('schedules') as FormArray;
  }

  save() {
    console.log(this.siteForm.value);
    this.siteService
      .addSite(this.siteForm.value)
      .pipe(
        catchError((error) => {
          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['/sites']);
      });
    this.router.navigate(['/sites']);
  }
}
