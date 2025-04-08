import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, inject, input, OnInit, output, SimpleChanges } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { LegendCardComponent } from '../../../shared/components/legend-card/legend-card.component';
import { Observable, map } from 'rxjs';
import { Course } from '../../../core/models/course.interface';
import { CourseService } from '../../../core/services/course.service';
import { EquipmentService } from '../../../core/services/equipment.service';

@Component({
  selector: 'app-course-list-view',
  imports: [RouterLink, AsyncPipe, MatIconModule, MatButtonModule, CommonModule, LegendCardComponent],
  templateUrl: './course-list-view.component.html',
  styleUrl: './course-list-view.component.scss'
})
export class CourseListViewComponent implements OnInit{

  private readonly router = inject(Router);
  private readonly courseService = inject(CourseService);
  private readonly equipmentTypeService = inject(EquipmentService);
  courseList$: Observable<Course[]> = this.courseService.courses$;
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;
  equipmentTypeFilterId = input<any>();
  selectedOption = input<string>();

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
    if(this.selectedOption()){
      this.router.navigate(['/courses/edit',id], {queryParams: {from: 'optionView',optionId: this.selectedOption()}});
    } else {
      this.router.navigate(['/courses/edit',id]);
    } 
  }

  ngOnChanges(changes: SimpleChanges){
    if(this.equipmentTypeFilterId()) this.onEquipmentTypeChange();
    if(this.selectedOption()) this.onOptionSelected();
  }

  onEquipmentTypeChange(){
    if(this.equipmentTypeFilterId() != 0){
          this.courseList$ = this.courseList$.pipe(
            map((courses: Course[]) => 
              courses.filter(course => 
                course.equipmentType?.some((equipmentType) => equipmentType.id == this.equipmentTypeFilterId()))));
        } else {
          this.courseList$ = this.courseService.courses$;
        }
  }

  onOptionSelected(){
    if(this.selectedOption() != undefined){
      this.courseList$ = this.courseList$.pipe(
        map((courses: Course[]) => 
          courses.filter(course => 
            Object.keys(course.optionGrades ?? {}).includes(this.selectedOption()!))));
    }
  }

  onDelete(id: string){
    this.courseService.deleteCourse(id).subscribe({
      next: () => this.updateUI(),
      error: (e) => alert("Suppression impossible ! Le cours est liée à une autre entité !")
    })
  }
}
