import { Component, inject, OnInit } from '@angular/core';
import { ClassroomService } from '../../../core/services/classroom.service';
import { EquipmentService } from '../../../core/services/equipment.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-equipment-list-filter',
  imports: [AsyncPipe],
  templateUrl: './equipment-list-filter.component.html',
  styleUrl: './equipment-list-filter.component.scss',
})
export class EquipmentListFilterComponent implements OnInit {
  private equipmentService = inject(EquipmentService);
  private classroomService = inject(ClassroomService);

  public equipmentTypes$ = this.equipmentService.equipmentTypes$;
  public classrooms$ = this.classroomService.classrooms$;

  params = {};

  ngOnInit(): void {
    this.classroomService.getClassroomsByUniversity().subscribe();
    this.equipmentService.getEquipmentTypes().subscribe();
  }

  onClassroomChange(event: any) {
    const classroomId = event.target.value;

    this.params = { ...this.params, classroomId: classroomId };

    console.log(this.params);

    this.equipmentService.getEquipments(this.params).subscribe();
  }

  onEquipmentTypeChange(event: any) {
    const equipmentTypeId = event.target.value;

    this.params = { ...this.params, equipmentTypeId: equipmentTypeId };

    console.log(this.params);

    this.equipmentService.getEquipments(this.params).subscribe();
  }
}
