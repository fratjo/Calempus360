import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { SiteService } from '../../../core/services/site.service';
import { ClassroomService } from '../../../core/services/classroom.service';

@Component({
  selector: 'app-classroom-list',
  imports: [AsyncPipe, RouterLink],
  templateUrl: './classroom-list.component.html',
  styleUrl: './classroom-list.component.scss',
})
export class ClassroomListComponent {
  private readonly router = inject(Router);
  private readonly classroomService = inject(ClassroomService);
  classroomList$ = this.classroomService.classrooms$;

  onSelect(classroomId: string) {
    this.classroomService.setClassroom(classroomId).subscribe();
    sessionStorage.setItem('classroom', JSON.stringify(classroomId));
    this.router.navigate(['classroom', classroomId]);
  }

  onEdit(classroomId: string) {
    this.router.navigate(['classrooms/edit', classroomId]);
  }

  onDelete(classroomId: string) {
    this.classroomService.deleteClassroom(classroomId).subscribe();
  }
}
