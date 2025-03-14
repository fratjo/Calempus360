import { Component, inject, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, first, of } from 'rxjs';
import { EquipmentService } from '../../../../core/services/equipment.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-equipment-type-edit-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './equipment-type-edit-form.component.html',
  styleUrl: './equipment-type-edit-form.component.scss',
})
export class EquipmentTypeEditFormComponent implements OnInit {
  private readonly activeRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private equipmentService = inject(EquipmentService);

  public equipmentTypeForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.equipmentTypeForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      code: ['', Validators.required],
      description: ['', [Validators.required]],
    });
  }

  ngOnInit() {
    this.activeRoute.paramMap.subscribe((params) => {
      const equipmentTypeId = params.get('id');
      if (equipmentTypeId) {
        this.equipmentService
          .getEquipmentTypeById(equipmentTypeId)
          .pipe(first((x) => !!x))
          .subscribe((u) => this.equipmentTypeForm.patchValue(u));
      }
    });
  }

  cancel() {
    this.router.navigate(['equipment-types']);
  }

  save() {
    this.equipmentService
      .updateEquipmentType(this.equipmentTypeForm.value)
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
