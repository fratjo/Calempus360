import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
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
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { first, catchError, of } from 'rxjs';
import { SiteService } from '../../../core/services/site.service';

@Component({
  selector: 'app-site-edit-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatIconModule,
    RouterModule,
    MatButtonModule,
    CommonModule,
  ],
  templateUrl: './site-edit-form.component.html',
  styleUrl: './site-edit-form.component.scss',
})
export class SiteEditFormComponent implements OnInit {
  private readonly activeRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private siteService = inject(SiteService);
  public site$ = this.siteService.site$;
  public siteForm: FormGroup;

  public days = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];

  constructor(public fb: FormBuilder) {
    this.siteForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      address: ['', Validators.required],
      code: ['', Validators.required],
      phone: ['', Validators.required],
      schedules: this.fb.array([],Validators.required),
    });

    this.days.forEach((day, i) => {
      this.scheduleArray.push(
        this.fb.group({
          dayOfWeek:[i+1,null],
          timeStart:['',Validators.required],
          timeEnd:['',Validators.required],
        })
      );
    });
  }

  ngOnInit() {
    this.activeRoute.paramMap.subscribe((params) => {
      const siteId = params.get('id');
      if (siteId) {
        this.siteService
          .getSiteById(siteId)
          .pipe(first((x) => !!x))
          .subscribe((u) => this.siteForm.patchValue(u));
      }
    });
  }

  get scheduleArray(){
      return this.siteForm.get('schedules') as FormArray;
    }

  cancel() {
    // redirect to sites
    this.router.navigate(['/sites']);
  }

  save() {
    this.siteService
      .updateSite(this.siteForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error editing site', error);
          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['/sites']);
      });
    this.router.navigate(['/sites']);
  }
}
