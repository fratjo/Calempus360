import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Course } from '../models/course.interface';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  courses$ = new BehaviorSubject<Course[]>([]);
  constructor(private http: HttpClient) {}

  URL = 'http://localhost:5257/api/courses';

  getCourses() {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    let params = new HttpParams();
    params = params.append('universityId', universityId);
    return this.http.get<Course[]>(this.URL, { params }).subscribe({
      next: (courses) => this.courses$.next(courses),
    });
  }

  getCourseById(id: string) {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    let params = new HttpParams();
    params = params.append('universityId', universityId);
    return this.http.get<Course>(this.URL + `/${id}`, { params });
  }

  addCourse(course: Course) {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    let params = new HttpParams();
    params = params.append('academicYear', academicYearId);
    params = params.append('universityId', universityId);
    return this.http.post<Course>(this.URL, course, { params });
  }

  updateCourse(course: Course) {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    let params = new HttpParams();
    params = params.append('academicYear', academicYearId);
    params = params.append('universityId', universityId);
    return this.http.put<Course>(this.URL + `/${course.id!}`, course, { params });
  }

  deleteCourse(id: string) {
    return this.http.delete<Course>(this.URL + `/${id}`);
  }
}
