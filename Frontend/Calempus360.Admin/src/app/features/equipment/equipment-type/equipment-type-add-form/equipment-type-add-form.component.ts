import { Component, inject } from '@angular/core';
import { EquipmentService } from '../../../../core/services/equipment.service';
import { catchError, of } from 'rxjs';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-equipment-type-add-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './equipment-type-add-form.component.html',
  styleUrl: './equipment-type-add-form.component.scss',
})
export class EquipmentTypeAddFormComponent {
  private readonly router = inject(Router);
  private equipmentService = inject(EquipmentService);

  public equipmentTypeForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.equipmentTypeForm = this.fb.group({
      name: ['', Validators.required],
      code: ['', Validators.required],
      description: ['', [Validators.required]],
    });
  }

  cancel() {
    this.router.navigate(['equipment-types']);
  }

  save() {
    this.equipmentService
      .addEquipmentType(this.equipmentTypeForm.value)
      .pipe(
        catchError((error) => {
          console.error('Error adding equipment type', error);

          return of(null);
        }),
      )
      .subscribe(() => {
        this.router.navigate(['equipment-types']);
      });
    this.router.navigate(['equipment-types']);
  }
}
