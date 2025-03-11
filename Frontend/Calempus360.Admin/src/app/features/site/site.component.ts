import { Component, inject, OnInit } from '@angular/core';
import { SiteService } from '../../core/services/site.service';
import { AsyncPipe } from '@angular/common';
import { ClassroomService } from '../../core/services/classroom.service';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { ClassroomListComponent } from '../classroom/classroom-list/classroom-list.component';

@Component({
  selector: 'app-site',
  imports: [AsyncPipe, MatIconModule, MatButtonModule, ClassroomListComponent],
  templateUrl: './site.component.html',
  styleUrl: './site.component.scss',
})
export class SiteComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly siteService = inject(SiteService);
  private readonly classroomService = inject(ClassroomService);
  public site$ = this.siteService.site$;

  ngOnInit() {
    this.classroomService
      .getClassroomsBySite(JSON.parse(sessionStorage.getItem('site')!))
      .subscribe();
  }
}
