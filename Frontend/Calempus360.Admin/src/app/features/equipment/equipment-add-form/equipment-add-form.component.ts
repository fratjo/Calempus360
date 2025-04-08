import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  ReactiveFormsModule,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of } from 'rxjs';
import { EquipmentService } from '../../../core/services/equipment.service';
import { SiteService } from '../../../core/services/site.service';

@Component({
  selector: 'app-equipment-add-form',
  imports: [CommonModule, ReactiveFormsModule, AsyncPipe],
  templateUrl: './equipment-add-form.component.html',
  styleUrl: './equipment-add-form.component.scss',
})
export class EquipmentAddFormComponent {
  private readonly router = inject(Router);
  private equipmentService = inject(EquipmentService);
  private siteService = inject(SiteService);
  public equipmentTypes$ = this.equipmentService.equipmentTypes$;
  public sites$ = this.siteService.sites$;
  public equipmentForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.equipmentForm = this.fb.group({
      name: ['', Validators.required],
      code: ['', Validators.required],
      brand: ['', Validators.required],
      model: ['', Validators.required],
      description: ['', [Validators.required]],
      equipmentTypeId: ['', Validators.required],
      siteId: ['', Validators.required],
    });
  }

  cancel() {
    this.router.navigate(['equipments']);
  }

  save() {
    this.equipmentService
      .addEquipment(this.equipmentForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error adding equipment', error);

          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['equipments']);
      });
    this.router.navigate(['equipments']);
  }
}
