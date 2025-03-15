import { Component, inject } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, first, of } from 'rxjs';
import { EquipmentService } from '../../../core/services/equipment.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-equipment-edit-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './equipment-edit-form.component.html',
  styleUrl: './equipment-edit-form.component.scss',
})
export class EquipmentEditFormComponent {
  private readonly activeRoute = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private equipmentService = inject(EquipmentService);
  public equipmentTypes$ = this.equipmentService.equipmentTypes$;
  public equipmentForm: FormGroup;

  constructor(public fb: FormBuilder) {
    this.equipmentForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      code: ['', Validators.required],
      brand: ['', Validators.required],
      model: ['', Validators.required],
      description: ['', [Validators.required]],
      equipmentTypeId: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.activeRoute.paramMap.subscribe((params) => {
      const equipmentId = params.get('id');
      if (equipmentId) {
        this.equipmentService
          .getEquipmentById(equipmentId)
          .pipe(first((x) => !!x))
          .subscribe((u) => {
            this.equipmentForm.patchValue(u);

            // select options : set the selected value
            this.equipmentTypes$.subscribe((equipmentTypes) => {
              if (equipmentTypes.length) {
                const index = equipmentTypes.findIndex(
                  (x) => x.id === u.equipmentType?.id,
                );

                this.equipmentForm
                  .get('equipmentTypeId')!
                  .setValue(equipmentTypes[index].id);
              }
            });
          });
      }
    });
  }

  cancel() {
    this.router.navigate(['equipments']);
  }

  save() {
    this.equipmentService
      .updateEquipment(this.equipmentForm.value)
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
