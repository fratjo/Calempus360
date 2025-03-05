import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
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
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { catchError, first, of } from 'rxjs';
import { ClassroomService } from '../../../core/services/classroom.service';
import { SiteService } from '../../../core/services/site.service';
import { UniversityService } from '../../../core/services/university.service';

@Component({
  selector: 'app-classroom-edit-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
    MatButtonModule,
    CommonModule,
  ],
  templateUrl: './classroom-edit-form.component.html',
  styleUrl: './classroom-edit-form.component.scss',
})
export class ClassroomEditFormComponent implements OnInit {
  private readonly activeRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private universityService = inject(UniversityService);
  private siteService = inject(SiteService);
  private classroomService = inject(ClassroomService);
  public classroomForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.classroomForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      code: ['', Validators.required],
      capacity: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.activeRoute.paramMap.subscribe((params) => {
      const classroomId = params.get('id');
      if (classroomId) {
        this.classroomService
          .getClassroomById(classroomId)
          .pipe(first((x) => !!x))
          .subscribe((u) => this.classroomForm.patchValue(u));
      }
    });
  }

  cancel() {
    this.siteService.site$.value.id;
    this.router.navigate(['/site', this.siteService.site$.value.id]);
  }

  save() {
    this.classroomService
      .updateClassroom(this.classroomForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error adding classroom', error);

          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['/site', this.siteService.site$.value.id]);
      });
    this.router.navigate(['/site', this.siteService.site$.value.id]);
  }
}
