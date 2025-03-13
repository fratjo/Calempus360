import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { EquipmentService } from '../../../core/services/equipment.service';

@Component({
  selector: 'app-equipment-list',
  imports: [AsyncPipe, RouterLink],
  templateUrl: './equipment-list.component.html',
  styleUrl: './equipment-list.component.scss',
})
export class EquipmentListComponent {
  private readonly router = inject(Router);
  private readonly equipmentService = inject(EquipmentService);
  equipmentList$ = this.equipmentService.equipments$;

  onSelect(equipmentId: string) {
    this.equipmentService.setEquipment(equipmentId).subscribe();
    sessionStorage.setItem('equipment', JSON.stringify(equipmentId));
    this.router.navigate(['equipment', equipmentId]);
  }

  onEdit(equipmentId: string) {
    this.router.navigate(['equipment/edit', equipmentId]);
  }

  onDelete(equipmentId: string) {
    this.equipmentService.deleteEquipment(equipmentId).subscribe();
  }
}
