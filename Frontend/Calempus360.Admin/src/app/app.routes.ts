import { Routes } from '@angular/router';
import { UniversityComponent } from './features/university/university.component';
import { SiteComponent } from './features/site/site.component';
import { Component } from '@angular/core';
import { UniversityAddFormComponent } from './features/university/university-add-form/university-add-form.component';
import { UniversitySwitchComponent } from './features/university/university-switch/university-switch.component';
import { univeristyGuard } from './core/guards/univeristy.guard';
import { AcademicYearAddFormComponent } from './features/academic-year/academic-year-add-form/academic-year-add-form.component';
import { AcademicYearComponent } from './features/academic-year/academic-year.component';
import { AcademicYearSwitchComponent } from './features/academic-year/academic-year-switch/academic-year-switch.component';
import { academicYearGuard } from './core/guards/academic-year.guard';

export const routes: Routes = [
  {
    path: 'university',
    component: UniversityComponent,
    canActivate: [univeristyGuard, academicYearGuard],
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
    path: 'academic-year',
    component: AcademicYearComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'academic-year/add',
    component: AcademicYearAddFormComponent,
  },
  {
    path: 'academic-year/change',
    component: AcademicYearSwitchComponent,
  },
  {
    path: 'sites',
    component: SiteComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: '**',
    redirectTo: 'university',
  },
];
