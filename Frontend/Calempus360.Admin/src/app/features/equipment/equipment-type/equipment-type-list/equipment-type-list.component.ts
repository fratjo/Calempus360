import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { EquipmentService } from '../../../../core/services/equipment.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-equipment-type-list',
  imports: [AsyncPipe, RouterLink],
  templateUrl: './equipment-type-list.component.html',
  styleUrl: './equipment-type-list.component.scss',
})
export class EquipmentTypeListComponent {
  private readonly router = inject(Router);
  private readonly equipmentService = inject(EquipmentService);
  equipmentTypeList$ = this.equipmentService.equipmentTypes$;

  onSelect(equipmentTypeId: string) {
    this.equipmentService.setEquipment(equipmentTypeId).subscribe();
    sessionStorage.setItem('equipment', JSON.stringify(equipmentTypeId));
    this.router.navigate(['equipment', equipmentTypeId]);
  }

  onEdit(equipmentTypeId: string) {
    this.router.navigate(['equipment/edit', equipmentTypeId]);
  }

  onDelete(equipmentTypeId: string) {
    this.equipmentService.deleteEquipment(equipmentTypeId).subscribe();
  }
}
