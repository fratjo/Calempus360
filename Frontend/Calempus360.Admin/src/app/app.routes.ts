import { Routes } from '@angular/router';
import { UniversityComponent } from './features/university/university.component';
import { SiteComponent } from './features/site/site.component';

export const routes: Routes = [
  {
    path: 'university',
    component: UniversityComponent,
  },
  {
    path: 'sites',
    component: SiteComponent,
  },
  {
    path: '**',
    redirectTo: 'university',
  },
];
