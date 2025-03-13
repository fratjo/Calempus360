import { Component, inject, OnInit } from '@angular/core';
import { EquipmentListComponent } from '../equipment-list/equipment-list.component';
import { EquipmentTypeListComponent } from '../equipment-type/equipment-type-list/equipment-type-list.component';
import { EquipmentListFilterComponent } from '../equipment-list-filter/equipment-list-filter.component';
import { EquipmentService } from '../../../core/services/equipment.service';

@Component({
  selector: 'app-equipment-list-page',
  imports: [
    EquipmentListComponent,
    EquipmentTypeListComponent,
    EquipmentListFilterComponent,
  ],
  templateUrl: './equipment-list-page.component.html',
  styleUrl: './equipment-list-page.component.scss',
})
export class EquipmentListPageComponent implements OnInit {
  private readonly equipmentService = inject(EquipmentService);

  ngOnInit(): void {
    this.equipmentService.getEquipmentTypes().subscribe();
    this.equipmentService.getEquipmentsByUniversity().subscribe();
  }
}
