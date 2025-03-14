import { Component, inject, OnInit } from '@angular/core';
import { ClassroomService } from '../../../core/services/classroom.service';
import { EquipmentService } from '../../../core/services/equipment.service';
import { AsyncPipe } from '@angular/common';
import { SiteService } from '../../../core/services/site.service';
import { BehaviorSubject, Observable } from 'rxjs';
import {
  Classroom,
  Classrooms,
} from '../../../core/models/classroom.interface';

@Component({
  selector: 'app-equipment-list-filter',
  imports: [AsyncPipe],
  templateUrl: './equipment-list-filter.component.html',
  styleUrl: './equipment-list-filter.component.scss',
})
export class EquipmentListFilterComponent implements OnInit {
  private siteService = inject(SiteService);
  private equipmentService = inject(EquipmentService);
  private classroomService = inject(ClassroomService);

  public equipmentTypes$ = this.equipmentService.equipmentTypes$;
  public classrooms$ = this.classroomService.classrooms$;
  public sites$ = this.siteService.sites$;

  params = {
    classroomId: '',
    equipmentTypeId: '',
    siteId: '',
    universityId: JSON.parse(sessionStorage.getItem('university')!),
  };

  ngOnInit(): void {
    this.siteService.getSites().subscribe();
    this.classroomService.getClassroomsByUniversity().subscribe();
    this.equipmentService.getEquipmentTypes().subscribe();
  }

  onClassroomChange(event: any) {
    const classroomId = event.target.value;

    this.params = { ...this.params, classroomId: classroomId };

    if (classroomId === '0') {
      this.params = { ...this.params, classroomId: '' };
    }

    console.log(this.params);

    this.equipmentService.getEquipments(this.params).subscribe();
  }

  onEquipmentTypeChange(event: any) {
    const equipmentTypeId = event.target.value;

    this.params = { ...this.params, equipmentTypeId: equipmentTypeId };

    if (equipmentTypeId === '0') {
      this.params = { ...this.params, equipmentTypeId: '' };
    }

    console.log(this.params);

    this.equipmentService.getEquipments(this.params).subscribe();
  }

  onSiteChange(event: any) {
    const siteId = event.target.value;

    this.params = { ...this.params, siteId: siteId };

    if (siteId === '0') {
      this.params = { ...this.params, siteId: '' };
    }

    console.log(this.params);

    this.equipmentService.getEquipments(this.params).subscribe();
    this.classrooms$ = this.classroomService.getClassroomsBySite(
      siteId,
    ) as BehaviorSubject<Classrooms>;
  }
}
