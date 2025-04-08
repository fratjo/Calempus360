import { Component, inject, OnInit } from '@angular/core';
import { EquipmentListComponent } from '../equipment-list/equipment-list.component';
import { EquipmentListFilterComponent } from '../equipment-list-filter/equipment-list-filter.component';
import { EquipmentService } from '../../../core/services/equipment.service';
import { RouterLink } from '@angular/router';
import { UniversityService } from '../../../core/services/university.service';

@Component({
  selector: 'app-equipment-list-page',
  imports: [EquipmentListComponent, EquipmentListFilterComponent, RouterLink],
  templateUrl: './equipment-list-page.component.html',
  styleUrl: './equipment-list-page.component.scss',
})
export class EquipmentListPageComponent implements OnInit {
  private readonly universityService = inject(UniversityService);
  private readonly equipmentService = inject(EquipmentService);

  ngOnInit(): void {
    this.equipmentService.getEquipmentTypes().subscribe();
    this.equipmentService
      .getEquipments({
        universityId: this.universityService.university$.value.id,
      })
      .subscribe();
  }
}
