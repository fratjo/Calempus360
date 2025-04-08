import { Component, inject, OnInit } from '@angular/core';
import { CourseService } from '../../core/services/course.service';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { EquipmentService } from '../../core/services/equipment.service';
import { Course } from '../../core/models/course.interface';
import { map, Observable } from 'rxjs';
import { CourseFilterViewComponent } from './course-filter-view/course-filter-view.component';
import { CourseListViewComponent } from './course-list-view/course-list-view.component';

@Component({
  selector: 'app-course',
  imports: [MatIconModule, MatButtonModule, CommonModule,CourseFilterViewComponent,CourseListViewComponent],
  templateUrl: './course.component.html',
  styleUrl: './course.component.scss'
})
export class CourseComponent implements OnInit{
  
  private readonly router = inject(Router);
  private readonly courseService = inject(CourseService);
  private readonly equipmentTypeService = inject(EquipmentService);
  courseList$: Observable<Course[]> = this.courseService.courses$;
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;
  equipmentTypeId: any = 0;

  ngOnInit(): void {
    this.updateUI();
    this.equipmentTypeService.getEquipmentTypes().subscribe();
  }

  updateUI(){
    this.courseService.getCourses();
  }

  onSelect(id: string){
    this.router.navigate(['/courses/view',id]);
  }

  onEdit(id: string){
    this.router.navigate(['/courses/edit',id]);
  }

  onEquipmentTypeChange(equipmentTypeId: string){
    console.log(equipmentTypeId);
    this.equipmentTypeId = equipmentTypeId;
  }

  onDelete(id: string){
    this.courseService.deleteCourse(id).subscribe({
      next: () => this.updateUI(),
      error: (e) => alert("Suppression impossible ! Le cours est liée à une autre entité !")
    })
  }
}
