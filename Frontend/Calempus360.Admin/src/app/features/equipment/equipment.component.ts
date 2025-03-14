import { Component, inject } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Observable } from 'rxjs';
import { Equipment } from '../../core/models/equipment.interface';
import { EquipmentService } from '../../core/services/equipment.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-equipment',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatIconModule],
  templateUrl: './equipment.component.html',
  styleUrl: './equipment.component.scss',
})
export class EquipmentComponent {
  private equipmentService = inject(EquipmentService);

  public equipment$ = this.equipmentService.equipment$;
  public equipmentTypes$ = this.equipmentService.equipmentTypes$;

  public editMode = false;
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

  edit() {
    this.editMode = !this.editMode;
    if (this.editMode) {
      this.equipment$.subscribe((equipment) => {
        this.equipmentForm.patchValue({
          id: equipment.id,
          name: equipment.name,
          code: equipment.code,
          brand: equipment.brand,
          model: equipment.model,
          description: equipment.description,
        });

        // select options : set the selected value
        this.equipmentTypes$.subscribe((equipmentTypes) => {
          if (equipmentTypes.length) {
            const index = equipmentTypes.findIndex(
              (x) => x.id === equipment.equipmentType?.id,
            );

            this.equipmentForm
              .get('equipmentTypeId')!
              .setValue(equipmentTypes[index].id);
          }
        });
      });
    }
  }

  cancel() {
    this.editMode = !this.editMode;
  }

  save() {
    this.editMode = !this.editMode;
    if (this.equipmentForm.valid) {
      this.equipmentService
        .updateEquipment(this.equipmentForm.value)
        .subscribe();
    }
  }
}
