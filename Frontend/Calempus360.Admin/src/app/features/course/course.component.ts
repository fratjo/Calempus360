import { Component, inject, OnInit } from '@angular/core';
import { CourseService } from '../../core/services/course.service';
import { Router, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { LegendCardComponent } from "../../shared/components/legend-card/legend-card.component";
import { EquipmentService } from '../../core/services/equipment.service';
import { Course } from '../../core/models/course.interface';
import { map, Observable } from 'rxjs';
import { EquipmentType } from '../../core/models/equipment.interface';

@Component({
  selector: 'app-course',
  imports: [RouterLink, AsyncPipe, MatIconModule, MatButtonModule, CommonModule, LegendCardComponent],
  templateUrl: './course.component.html',
  styleUrl: './course.component.scss'
})
export class CourseComponent implements OnInit{
  
  private readonly router = inject(Router);
  private readonly courseService = inject(CourseService);
  private readonly equipmentTypeService = inject(EquipmentService);
  courseList$: Observable<Course[]> = this.courseService.courses$;
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;

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

  onEquipmentTypeChange(event: any){
    if(event.target.value != 0){
          this.courseList$ = this.courseList$.pipe(
            map((courses: Course[]) => 
              courses.filter(course => 
                course.equipmentType?.some((equipmentType) => equipmentType.id == event.target.value))));
        } else {
          this.courseList$ = this.courseService.courses$;
        }
  }

  onDelete(id: string){
    this.courseService.deleteCourse(id).subscribe({
      next: () => this.updateUI(),
      error: (e) => alert("Suppression impossible ! Le cours est liée à une autre entité !")
    })
  }
}
