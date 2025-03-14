import { Component } from '@angular/core';
import { EquipmentTypeListComponent } from '../equipment-type-list/equipment-type-list.component';

@Component({
  selector: 'app-equipment-type-list-page',
  imports: [EquipmentTypeListComponent],
  templateUrl: './equipment-type-list-page.component.html',
  styleUrl: './equipment-type-list-page.component.scss',
})
export class EquipmentTypeListPageComponent {}
