import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { OptionService } from '../../core/services/option.service';
import { map, Observable } from 'rxjs';
import { Option } from '../../core/models/option.interface';
import { LegendCardComponent } from "../../shared/components/legend-card/legend-card.component";
import { CourseService } from '../../core/services/course.service';
import { Course } from '../../core/models/course.interface';
import { OptionFilterViewComponent } from "./option-filter-view/option-filter-view.component";
import { OptionListViewComponent } from "./option-list-view/option-list-view.component";

@Component({
  selector: 'app-option',
  imports: [MatIconModule, MatButtonModule, OptionFilterViewComponent, OptionListViewComponent],
  templateUrl: './option.component.html',
  styleUrl: './option.component.scss'
})
export class OptionComponent {
  private readonly router = inject(Router);
  private readonly optionService = inject(OptionService);
  private readonly courseService = inject(CourseService);

  options$: Observable<Option[]> = this.optionService.options$;
  course$: Observable<Course[]> = this.courseService.courses$;

  courseId: any = 0;

  ngOnInit(): void {
    this.updateUI();
  }

  updateUI(){
    this.optionService.getOptions();
    this.courseService.getCourses();
  }

  onSelect(id: string){
    this.router.navigate(['/options/view',id]);
  }

  onEdit(id: string){
    this.router.navigate(['/options/edit',id]);
  }

  onDelete(id: string){
    this.optionService.deleteOption(id).subscribe({
      next: () => this.updateUI(),
      error: (e) => alert("Suppression impossible ! L'option est liée à une autre entité !")
    }); 
  }

  onCourseChange(courseId: any){
    this.courseId = courseId;
  }

}
