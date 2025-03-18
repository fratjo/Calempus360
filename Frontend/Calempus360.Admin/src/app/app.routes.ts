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
import { EquipmentAddFormComponent } from './features/equipment/equipment-add-form/equipment-add-form.component';
import { EquipmentEditFormComponent } from './features/equipment/equipment-edit-form/equipment-edit-form.component';
import { EquipmentComponent } from './features/equipment/equipment.component';
import { EquipmentTypeAddFormComponent } from './features/equipment/equipment-type/equipment-type-add-form/equipment-type-add-form.component';
import { EquipmentTypeEditFormComponent } from './features/equipment/equipment-type/equipment-type-edit-form/equipment-type-edit-form.component';
import { EquipmentTypeListPageComponent } from './features/equipment/equipment-type/equipment-type-list-page/equipment-type-list-page.component';
import { EquipmentTypeComponent } from './features/equipment/equipment-type/equipment-type.component';
import { StudentGroupsComponent } from './features/student-groups/student-groups.component';
import { StudentGroupAddFormComponent } from './features/student-groups/student-group-add-form/student-group-add-form.component';
import { StudentGroupEditFormComponent } from './features/student-groups/student-group-edit-form/student-group-edit-form.component';
import { OptionComponent } from './features/option/option.component';
import { OptionAddFormComponent } from './features/option/option-add-form/option-add-form.component';
import { OptionEditFormComponent } from './features/option/option-edit-form/option-edit-form.component';
import { CourseComponent } from './features/course/course.component';
import { CourseAddFormComponent } from './features/course/course-add-form/course-add-form.component';
import { CourseEditFormComponent } from './features/course/course-edit-form/course-edit-form.component';
import { StudentGroupViewComponent } from './features/student-groups/student-group-view/student-group-view.component';
import { OptionViewComponent } from './features/option/option-view/option-view.component';
import { CourseViewComponent } from './features/course/course-view/course-view.component';

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
    path: 'equipment/:id',
    component: EquipmentComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'equipments/add',
    component: EquipmentAddFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'equipments/edit/:id',
    component: EquipmentEditFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'equipment-types',
    component: EquipmentTypeListPageComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'equipment-type/:id',
    component: EquipmentTypeComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'equipment-types/add',
    component: EquipmentTypeAddFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'equipment-types/edit/:id',
    component: EquipmentTypeEditFormComponent,
  },
  {
    path: 'groups',
    component: StudentGroupsComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'groups/view/:id',
    component: StudentGroupViewComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'groups/add',
    component: StudentGroupAddFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'groups/edit/:id',
    component: StudentGroupEditFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'options',
    component: OptionComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'options/add',
    component: OptionAddFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'options/view/:id',
    component: OptionViewComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'options/edit/:id',
    component: OptionEditFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'courses',
    component: CourseComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'courses/view/:id',
    component: CourseViewComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'courses/add',
    component: CourseAddFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: 'courses/edit/:id',
    component: CourseEditFormComponent,
    canActivate: [univeristyGuard, academicYearGuard],
  },
  {
    path: '**',
    redirectTo: 'home',
  },
];
