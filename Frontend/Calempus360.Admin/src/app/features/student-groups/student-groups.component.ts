import { Component, inject, OnInit } from '@angular/core';
import { StudentGroupsService } from '../../core/services/student-groups.service';
import { Router, RouterLink } from '@angular/router';
import { StudentGroup } from '../../core/models/student-group.interface';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-student-groups',
  imports: [RouterLink, AsyncPipe, MatIconModule, MatButtonModule],
  templateUrl: './student-groups.component.html',
  styleUrl: './student-groups.component.scss'
})
export class StudentGroupsComponent implements OnInit{

  private readonly router = inject(Router);
  private readonly studentGroupService = inject(StudentGroupsService);

  studentGroups$: Observable<StudentGroup[]> = this.studentGroupService.studentGroups$;
  

  ngOnInit(): void {
    this.studentGroupService.getStudentGroups().subscribe();
  }

}
