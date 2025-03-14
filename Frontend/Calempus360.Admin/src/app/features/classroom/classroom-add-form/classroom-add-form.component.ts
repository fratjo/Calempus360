import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  ReactiveFormsModule,
  FormsModule,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule, Router } from '@angular/router';
import { catchError, of, retry } from 'rxjs';
import { SiteService } from '../../../core/services/site.service';
import { UniversityService } from '../../../core/services/university.service';
import { ClassroomService } from '../../../core/services/classroom.service';

@Component({
  selector: 'app-classroom-add-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
    MatButtonModule,
    CommonModule,
  ],
  templateUrl: './classroom-add-form.component.html',
  styleUrl: './classroom-add-form.component.scss',
})
export class ClassroomAddFormComponent {
  private readonly router = inject(Router);
  private siteService = inject(SiteService);
  private classroomService = inject(ClassroomService);

  public sites$ = this.siteService.sites$;

  public classroomForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.classroomForm = this.fb.group({
      siteId: ['', Validators.required],
      name: ['', Validators.required],
      code: ['', Validators.required],
      capacity: ['20', [Validators.required]],
    });
  }

  cancel() {
    this.router.navigate(['classrooms']);
  }

  save() {
    this.classroomService
      .addClassroom(this.classroomForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error adding classroom', error);

          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['classrooms']);
      });
    this.router.navigate(['classrooms']);
  }
}
