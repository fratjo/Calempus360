import { Component, inject, OnInit } from '@angular/core';
import { SiteService } from '../../../core/services/site.service';
import { AsyncPipe } from '@angular/common';
import { ClassroomService } from '../../../core/services/classroom.service';

@Component({
  selector: 'app-classroom-list-filter',
  imports: [AsyncPipe],
  templateUrl: './classroom-list-filter.component.html',
  styleUrl: './classroom-list-filter.component.scss',
})
export class ClassroomListFilterComponent implements OnInit {
  private readonly siteService = inject(SiteService);
  private readonly ClassroomService = inject(ClassroomService);

  public sites$ = this.siteService.sites$;

  ngOnInit(): void {
    this.siteService.getSites().subscribe();
  }

  public onSiteChange(event: any): void {
    if (event.target.value == 0) {
      this.ClassroomService.getClassrooms({
        universityId: JSON.parse(sessionStorage.getItem('university')!),
      }).subscribe();
    } else {
      this.ClassroomService.getClassrooms({
        siteId: event.target.value,
      }).subscribe();
    }
  }
}
