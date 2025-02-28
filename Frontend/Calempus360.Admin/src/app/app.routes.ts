import { Routes } from '@angular/router';
import { UniversityComponent } from './features/university/university.component';
import { SiteComponent } from './features/site/site.component';
import { Component } from '@angular/core';
import { UniversityAddFormComponent } from './features/university/university-add-form/university-add-form.component';
import { UniversitySwitchComponent } from './features/university/university-switch/university-switch.component';

export const routes: Routes = [
  {
    path: 'university',
    component: UniversityComponent,
  },
  {
    path: 'university/add',
    component: UniversityAddFormComponent,
  },
  {
    path: 'university/change',
    component: UniversitySwitchComponent,
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
