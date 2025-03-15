import { Component, inject, OnInit, signal } from '@angular/core';
import { TopBarComponent } from './layout/top-bar/top-bar.component';
import { UniversityService } from './core/services/university.service';
import {
  Router,
  RouterLink,
  RouterOutlet,
  RoutesRecognized,
} from '@angular/router';
import { DockBarComponent } from './layout/dock-bar/dock-bar.component';
import { SideMenuComponent } from './layout/side-menu/side-menu.component';
import { SiteService } from './core/services/site.service';
import { ClassroomService } from './core/services/classroom.service';
import { AcademicYearService } from './core/services/academic-year.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [TopBarComponent, SideMenuComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  private readonly universityService = inject(UniversityService);
  public university$ = this.universityService.university$;
  private readonly siteService = inject(SiteService);
  public site$ = this.siteService.site$;
  private readonly classroomService = inject(ClassroomService);
  public classroom$ = this.classroomService.classroom$;
  private readonly academicService = inject(AcademicYearService);
  public academicYear$ = this.academicService.academicYear$;

  private readonly router = inject(Router);

  ngOnInit() {
    if (sessionStorage.getItem('university')) {
      this.universityService
        .setUniversity(JSON.parse(sessionStorage.getItem('university')!))
        .subscribe();
    }

    if (sessionStorage.getItem('academicYear')) {
      this.academicService
        .setAcademicYear(JSON.parse(sessionStorage.getItem('academicYear')!))
        .subscribe();
    }

    this.router.navigate(['home']);
  }

  public currentRoute = signal<string>('');

  constructor() {
    this.router.events.subscribe((event) => {
      const breadcrumbs = document.getElementById(
        'breadcrumbs',
      )! as HTMLElement;
    });
  }
}
