import { Routes } from '@angular/router';
import { UniversityComponent } from './features/university/university.component';
import { SiteComponent } from './features/site/site.component';
import { StudentGroupsComponent } from './features/student-groups/student-groups.component';

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
    path: 'groups',
    component: StudentGroupsComponent
  },
  {
    path: '**',
    redirectTo: 'university',
  },
];
