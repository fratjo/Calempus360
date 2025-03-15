import { Component, inject, OnInit } from '@angular/core';
import { ClassroomListComponent } from '../classroom-list/classroom-list.component';
import { ClassroomListFilterComponent } from '../classroom-list-filter/classroom-list-filter.component';
import { ClassroomService } from '../../../core/services/classroom.service';

@Component({
  selector: 'app-classroom-list-page',
  imports: [ClassroomListComponent, ClassroomListFilterComponent],
  templateUrl: './classroom-list-page.component.html',
  styleUrl: './classroom-list-page.component.scss',
})
export class ClassroomListPageComponent implements OnInit {
  private readonly classroomService = inject(ClassroomService);

  ngOnInit(): void {
    this.classroomService
      .getClassrooms({
        universityId: JSON.parse(sessionStorage.getItem('university')!),
      })
      .subscribe();
  }
}
