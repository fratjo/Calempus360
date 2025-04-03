import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit, output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { LegendCardComponent } from '../../../shared/components/legend-card/legend-card.component';
import { CourseService } from '../../../core/services/course.service';

@Component({
  selector: 'app-option-filter-view',
  imports: [AsyncPipe, MatIconModule, MatButtonModule, LegendCardComponent],
  templateUrl: './option-filter-view.component.html',
  styleUrl: './option-filter-view.component.scss'
})
export class OptionFilterViewComponent implements OnInit{
  private readonly courseService = inject(CourseService);
  course$ = this.courseService.courses$;
  courseId = output<any>();

  ngOnInit(): void {
    this.courseService.getCourses();
  }

  onCourseChange(event: any){
    this.courseId.emit(event.target.value);
  }

}
