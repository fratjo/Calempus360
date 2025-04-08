import { AsyncPipe } from '@angular/common';
import { Component, inject, input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { LegendCardComponent } from '../../../shared/components/legend-card/legend-card.component';
import { Observable, map } from 'rxjs';
import { Course } from '../../../core/models/course.interface';
import { CourseService } from '../../../core/services/course.service';
import { OptionService } from '../../../core/services/option.service';
import { Option } from '../../../core/models/option.interface';

@Component({
  selector: 'app-option-list-view',
  imports: [RouterLink, AsyncPipe, MatIconModule, MatButtonModule, LegendCardComponent],
  templateUrl: './option-list-view.component.html',
  styleUrl: './option-list-view.component.scss'
})
export class OptionListViewComponent implements OnInit, OnChanges{
  private readonly router = inject(Router);
  private readonly optionService = inject(OptionService);
  private readonly courseService = inject(CourseService); 
  options$: Observable<Option[]> = this.optionService.options$;
  course$: Observable<Course[]> = this.courseService.courses$;
  courseFilterId = input<any>();
  selectedCourseId = input<string>();

  ngOnInit(): void {
    this.updateUI();
  }

  ngOnChanges(changes: SimpleChanges) {
    if(this.selectedCourseId()) this.onCourseSelected();
    if(this.courseFilterId()) this.onCourseChange();
  }

  updateUI(){
    this.optionService.getOptions();
    this.courseService.getCourses();
  }

  onSelect(id: string){
    this.router.navigate(['/options/view',id]);
  }

  onEdit(id: string){
    if(this.selectedCourseId()) this.router.navigate(['/options/edit',id], {queryParams: {from: 'courseView', courseId: this.selectedCourseId()}});
    else this.router.navigate(['/options/edit',id]);
  }

  onDelete(id: string){
    this.optionService.deleteOption(id).subscribe({
      next: () => this.updateUI(),
      error: (e) => alert("Suppression impossible ! L'option est liée à une autre entité !")
    }); 
  }

  onCourseChange(){
    if(this.courseFilterId() != 0){
      this.options$ = this.options$.pipe(
        map((options: Option[]) => 
          options.filter(option => 
            option.courses?.some((course) => course.id == this.courseFilterId()))));
    } else {
      this.options$ = this.optionService.options$;
    } 
  }

  onCourseSelected(){
    if(this.selectedCourseId()){
      this.options$ = this.options$.pipe(
        map((options: Option[]) => 
          options.filter(option => 
            option.courses?.some((course) => course.id == this.selectedCourseId()))));
    }
  }
}
