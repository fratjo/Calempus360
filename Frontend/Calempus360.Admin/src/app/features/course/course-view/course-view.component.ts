import { Component, inject, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { LegendCardComponent } from '../../../shared/components/legend-card/legend-card.component';
import { CourseService } from '../../../core/services/course.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Course } from '../../../core/models/course.interface';
import { CommonModule } from '@angular/common';
import { EquipmentService } from '../../../core/services/equipment.service';
import { EquipmentType } from '../../../core/models/equipment.interface';
import { map } from 'rxjs';

@Component({
  selector: 'app-course-view',
  imports: [MatIconModule, LegendCardComponent, CommonModule],
  templateUrl: './course-view.component.html',
  styleUrl: './course-view.component.scss'
})
export class CourseViewComponent implements OnInit{
  
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly courseService = inject(CourseService);
  private readonly equipmentTypeService = inject(EquipmentService);
  course: Course | undefined;
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;
  courseEquipmentType: EquipmentType[] | undefined;

  ngOnInit(): void {
    this.equipmentTypeService.getEquipmentTypes().subscribe();
    this.equipmentTypeList$.pipe(
      map((equipmentType: EquipmentType[]) => equipmentType.filter(equipmentType =>{
        for(let i = 0; i < this.course!.equipmentType!.length;i++){
          return equipmentType.id == this.course?.equipmentType![i];
        }
        return false;
      }))
    )
    this.route.paramMap.subscribe(params => {
      this.courseService.getCourseById(params.get('id')!).subscribe({
        next: (course) => this.course = course
      })
  });
  }

  onEdit(id: string){
    this.router.navigate(['/courses/edit',id], {queryParams: {from: 'details'}});
  }




}
