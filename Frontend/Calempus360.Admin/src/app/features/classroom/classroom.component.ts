import { Component, inject, OnInit } from '@angular/core';
import { ClassroomService } from '../../core/services/classroom.service';
import { AsyncPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { SiteService } from '../../core/services/site.service';
import { ClassroomListComponent } from './classroom-list/classroom-list.component';
import { EquipmentListComponent } from '../equipment/equipment-list/equipment-list.component';
import { EquipmentService } from '../../core/services/equipment.service';

@Component({
  selector: 'app-classroom',
  imports: [AsyncPipe, EquipmentListComponent],
  templateUrl: './classroom.component.html',
  styleUrl: './classroom.component.scss',
})
export class ClassroomComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly classroomService = inject(ClassroomService);
  private readonly equipmentService = inject(EquipmentService);
  public classroom$ = this.classroomService.classroom$;

  ngOnInit() {
    this.classroomService
      .setClassroom(JSON.parse(sessionStorage.getItem('classroom')!))
      .subscribe();

    this.classroom$.subscribe((classroom) => {
      if (classroom) {
        this.equipmentService
          .getEquipmentsByClassroom(classroom.id)
          .subscribe();
      }
    });
  }

  onSelect(id: string) {
    this.classroomService.setClassroom(id).subscribe();
    this.router.navigate(['classroom', id]);
  }

  onEdit(id: string) {
    this.router.navigate(['classrooms/edit', id]);
  }

  onDelete(id: string) {
    this.classroomService.deleteClassroom(id).subscribe();
  }
}
