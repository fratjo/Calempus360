import { Component, inject, OnInit } from '@angular/core';
import { UniversityService } from '../../core/services/university.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { University } from '../../core/models/university.interface';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-university',
  imports: [
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe,
  ],
  templateUrl: './university.component.html',
  styleUrl: './university.component.scss',
})
export class UniversityComponent {
  private universityService = inject(UniversityService);

  public university$: Observable<University> =
    this.universityService.university$;

  public editMode = false;
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

  edit() {
    this.editMode = !this.editMode;
    if (this.editMode) {
      this.university$.subscribe((university) => {
        this.universityForm.patchValue({
          id: university.id,
          name: university.name,
          address: university.address,
          code: university.code,
          phone: university.phone,
        });
      });
    }
  }

  cancel() {
    this.editMode = !this.editMode;
  }

  save() {
    this.editMode = !this.editMode;
    if (this.universityForm.valid) {
      //console.log('New University:', this.universityForm.value);
      this.universityService
        .updateUniversity(this.universityForm.value)
        .subscribe();
    }
  }
}
