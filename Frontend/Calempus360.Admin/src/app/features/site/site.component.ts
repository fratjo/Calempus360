import { Component, inject, OnInit } from '@angular/core';
import { SiteService } from '../../core/services/site.service';
import { AsyncPipe } from '@angular/common';
import { ClassroomService } from '../../core/services/classroom.service';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-site',
  imports: [AsyncPipe, MatIconModule, MatButtonModule, RouterLink],
  templateUrl: './site.component.html',
  styleUrl: './site.component.scss',
})
export class SiteComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly siteService = inject(SiteService);
  private readonly classroomService = inject(ClassroomService);
  public classrooms$ = this.classroomService.classrooms$;
  public site$ = this.siteService.site$;

  ngOnInit() {
    this.classroomService.getClassrooms().subscribe();
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
