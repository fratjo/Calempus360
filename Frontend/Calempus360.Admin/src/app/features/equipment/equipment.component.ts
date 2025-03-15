import { Component, inject, OnInit } from '@angular/core';
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
import { ClassroomService } from '../../core/services/classroom.service';
import { SiteService } from '../../core/services/site.service';

@Component({
  selector: 'app-equipment',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatIconModule],
  templateUrl: './equipment.component.html',
  styleUrl: './equipment.component.scss',
})
export class EquipmentComponent {
  private equipmentService = inject(EquipmentService);
  private classroomService = inject(ClassroomService);
  private siteService = inject(SiteService);

  public equipment$ = this.equipmentService.equipment$;
  public equipmentTypes$ = this.equipmentService.equipmentTypes$;
  public classrooms$ = this.classroomService.classrooms$;

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
      classroomId: [''],
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

        // select options : set the selected value
        if (equipment.classroom) {
          this.classroomService
            .getClassroomById(equipment.classroom?.id!)
            .subscribe((classroom) => {
              this.classroomService
                .getClassrooms({ siteId: classroom.site })
                .subscribe();
            });
        } else {
          this.siteService
            .getSiteByEquipmentId(equipment.id!)
            .subscribe((site) => {
              this.classroomService
                .getClassrooms({ siteId: site.id })
                .subscribe();
            });
        }
        this.classrooms$.subscribe((classroom) => {
          if (classroom.length) {
            const index = classroom.findIndex(
              (x) => x.id === equipment.classroom?.id,
            );

            this.equipmentForm
              .get('classroomId')!
              .setValue(classroom[index].id);
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
      console.log(this.equipmentForm.value);

      this.equipmentService
        .updateEquipment(this.equipmentForm.value)
        .subscribe();
    }
  }
}
