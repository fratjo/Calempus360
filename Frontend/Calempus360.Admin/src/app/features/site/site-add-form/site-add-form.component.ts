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

  constructor(public fb: FormBuilder) {
    this.siteForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      code: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }

  cancel() {
    // redirect to university
    this.router.navigate(['/sites']);
  }

  save() {
    this.siteService
      .addSite(this.siteForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error adding site', error);

          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['/sites']);
      });
    this.router.navigate(['/sites']);
  }
}
