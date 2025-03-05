import { Component, inject } from '@angular/core';
import { SiteService } from '../../core/services/site.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-site',
  imports: [AsyncPipe],
  templateUrl: './site.component.html',
  styleUrl: './site.component.scss',
})
export class SiteComponent {
  private readonly siteService = inject(SiteService);
  public site$ = this.siteService.site$;
}
