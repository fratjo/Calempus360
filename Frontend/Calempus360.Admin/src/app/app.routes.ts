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
import { HomeComponent } from './features/home/home.component';
import { UniversityEditFormComponent } from './features/university/university-edit-form/university-edit-form.component';
import { AcademicYearEditFormComponent } from './features/academic-year/academic-year-edit-form/academic-year-edit-form.component';
import { SiteListComponent } from './features/site/site-list/site-list.component';
import { SiteAddFormComponent } from './features/site/site-add-form/site-add-form.component';
import { SiteEditFormComponent } from './features/site/site-edit-form/site-edit-form.component';
import { ClassroomComponent } from './features/classroom/classroom.component';
import { ClassroomAddFormComponent } from './features/classroom/classroom-add-form/classroom-add-form.component';
import { ClassroomEditFormComponent } from './features/classroom/classroom-edit-form/classroom-edit-form.component';
import { ClassroomListComponent } from './features/classroom/classroom-list/classroom-list.component';
import { ClassroomListPageComponent } from './features/classroom/classroom-list-page/classroom-list-page.component';
import { SiteListPageComponent } from './features/site/site-list-page/site-list-page.component';
import { EquipmentListPageComponent } from './features/equipment/equipment-list-page/equipment-list-page.component';

export const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
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
    path: 'university/edit/:id',
    component: UniversityEditFormComponent,
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
    path: 'academic-year/edit/:id',
    component: AcademicYearEditFormComponent,
  },
  {
    path: 'academic-year/change',
    component: AcademicYearSwitchComponent,
  },
  {
    path: 'sites',
    component: SiteListPageComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'site/:id',
    component: SiteComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'sites/add',
    component: SiteAddFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'sites/edit/:id',
    component: SiteEditFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'classrooms',
    component: ClassroomListPageComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'classroom/:id',
    component: ClassroomComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'classrooms/add',
    component: ClassroomAddFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'classrooms/edit/:id',
    component: ClassroomEditFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'equipments',
    component: EquipmentListPageComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: '**',
    redirectTo: 'home',
  },
];
