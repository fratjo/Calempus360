import { Component, inject, OnInit } from '@angular/core';
import { SiteService } from '../../../core/services/site.service';
import { SiteListComponent } from '../site-list/site-list.component';

@Component({
  selector: 'app-site-list-page',
  imports: [SiteListComponent],
  templateUrl: './site-list-page.component.html',
  styleUrl: './site-list-page.component.scss',
})
export class SiteListPageComponent implements OnInit {
  private readonly siteService = inject(SiteService);

  ngOnInit(): void {
    this.siteService.getSites().subscribe();
  }
}
