import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Course } from '../models/course.interface';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  courses$ = new BehaviorSubject<Course[]>([]);
  constructor(private http: HttpClient) { }

  URL = 'http://localhost:5257/api/university/{universityId}/courses';

  getCourses(){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const url = this.URL.replace('{universityId}', universityId);
    return this.http.get<Course[]>(url).subscribe({
      next: (courses) => this.courses$.next(courses)
    })
  }

  getCourseById(id: string){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const url = this.URL.replace('{universityId}', universityId) + `/${id}`;
    return this.http.get<Course>(url);
  }

  addCourse(course: Course){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const url = this.URL.replace('{universityId}', universityId) + `?academicYear=${academicYearId}`;
    return this.http.post<Course>(url,course);
  }

  updateCourse(course: Course){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const url = this.URL.replace('{universityId}', universityId) + `/${course.id}` + `?academicYear=${academicYearId}` ;
    return this.http.put<Course>(url,course);
  }

  deleteCourse(id:string){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const url = this.URL.replace('{universityId}', universityId) + `/${id}`;
    return this.http.delete<Course>(url);
  }
}
