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
  courseList$ = this.courseService.courses$;

  ngOnInit(): void {
    this.updateUI();
  }

  updateUI(){
    this.courseService.getCourses();
  }

  onSelect(id: string){

  }

  onEdit(id: Course){
    console.log(id);
    this.router.navigate(['/courses/edit',id.id]);
  }

  onDelete(id: string){
    this.courseService.deleteCourse(id).subscribe({
      next: () => this.updateUI(),
      error: (e) => alert("Suppression impossible ! Le cours est liée à une autre entité !")
    })
  }

}
