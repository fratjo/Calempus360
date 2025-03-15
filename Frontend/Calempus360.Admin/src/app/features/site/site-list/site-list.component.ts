import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { SiteService } from '../../../core/services/site.service';
import { MatIconModule } from '@angular/material/icon';
import { AsyncPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-site-list',
  imports: [MatButtonModule, MatIconModule, AsyncPipe, RouterLink],
  templateUrl: './site-list.component.html',
  styleUrl: './site-list.component.scss',
})
export class SiteListComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly siteService = inject(SiteService);
  siteList$ = this.siteService.sites$;

  ngOnInit(): void {
    this.siteService.getSites().subscribe();
  }

  onSelect(siteId: string) {
    this.siteService.setSite(siteId).subscribe();
    sessionStorage.setItem('site', JSON.stringify(siteId));
    this.router.navigate(['site', siteId]);
  }

  onEdit(siteId: string) {
    this.router.navigate(['sites/edit', siteId]);
  }

  onDelete(siteId: string) {
    this.siteService.deleteSite(siteId).subscribe();
  }
}
