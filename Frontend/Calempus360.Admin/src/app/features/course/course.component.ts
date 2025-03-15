import { Component, inject, OnInit } from '@angular/core';
import { CourseService } from '../../core/services/course.service';
import { Router, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-course',
  imports: [RouterLink, AsyncPipe, MatIconModule, MatButtonModule, CommonModule],
  templateUrl: './course.component.html',
  styleUrl: './course.component.scss'
})
export class CourseComponent implements OnInit{
  
  private readonly router = inject(Router);
  private readonly courseService = inject(CourseService);

  courseList$ = this.courseService.courses$;

  ngOnInit(): void {
    this.updateUI();
  }

  updateUI(){
    this.courseService.getCourses();
  }

  onEdit(id: string){
    this.router.navigate(['/courses/edit',id]);
  }

  onDelete(id: string){
    this.courseService.deleteCourse(id).subscribe({
      next: () => this.updateUI(),
      error: (e) => alert("Suppression impossible ! Le cours est liée à une autre entité !")
    })
  }

}
