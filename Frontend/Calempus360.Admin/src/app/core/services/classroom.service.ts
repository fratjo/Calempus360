import { inject, Injectable } from '@angular/core';
import { Classroom, Classrooms } from '../models/classroom.interface';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { SiteService } from './site.service';
import { UniversityService } from './university.service';

@Injectable({
  providedIn: 'root',
})
export class ClassroomService {
  private http: HttpClient = inject(HttpClient);
  private universityService = inject(UniversityService);
  private siteService = inject(SiteService);
  public classroom$ = new BehaviorSubject<Classroom>({});
  public classrooms$ = new BehaviorSubject<Classrooms>([]);

  URL = 'http://localhost:5257/api/classrooms';

  isClassroom(): boolean {
    return !!this.classroom$.value && !!this.classroom$.value.id;
  }

  getClassrooms({
    universityId,
    siteId,
  }: {
    universityId?: string;
    siteId?: string;
  }) {
    let params = new HttpParams();

    if (universityId) {
      params = params.append('universityId', universityId);
    }

    if (siteId) {
      params = params.append('siteId', siteId);
    }

    return this.http.get<Classrooms>(this.URL, { params }).pipe(
      tap((s: Classrooms) => {
        this.classrooms$.next(s.sort((a, b) => a.code!.localeCompare(b.code!)));
      }),
    );
  }

  getClassroomById(id: string) {
    return this.http.get<Classroom>(this.URL + `/${id}`);
  }

  setClassroom(id: string) {
    return this.http.get<Classroom>(this.URL + `/${id}`).pipe(
      tap((s: Classroom) => {
        this.classroom$.next(s);
        sessionStorage.setItem('classroom', JSON.stringify(s.id));
      }),
    );
  }

  updateClassroom(classroom: Classroom) {
    return this.http
      .put<Classroom>(this.URL + `/${classroom.id}`, classroom)
      .pipe(
        tap((s: Classroom) => {
          const classrooms = this.classrooms$.value;
          const index = classrooms.findIndex(
            (classroom) => classroom.id === s.id,
          );
          classrooms[index] = s;
          this.classrooms$.next(classrooms);
        }),
        tap({
          error: (err) => {
            console.error('Error adding site:', err);
            // Handle the error as needed
            // For example, you can show a notification or log the error
            alert(`An error occurred while adding. ${err.error.title}`);
          },
        }),
      );
  }

  addClassroom(classroom: Classroom) {
    let params = new HttpParams().set('siteId', classroom.site!);

    return this.http
      .post<Classroom>(
        this.URL,
        {
          name: classroom.name,
          code: classroom.code,
          capacity: Number(classroom.capacity),
        },
        { params },
      )
      .pipe(
        tap((s: Classroom) => {
          this.classrooms$.next([...this.classrooms$.value, s]);
        }),
        tap({
          error: (err) => {
            console.error('Error adding site:', err);
            // Handle the error as needed
            // For example, you can show a notification or log the error
            alert(`An error occurred while adding. ${err.error.title}`);
          },
        }),
      );
  }

  deleteClassroom(id: string) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    const siteUrl = url.replace(
      '{siteId}',
      JSON.parse(sessionStorage.getItem('site')!),
    );

    return this.http.delete<Classroom>(siteUrl + `/${id}`).pipe(
      tap((s: Classroom) => {
        this.classrooms$.next(
          this.classrooms$.value.filter((classroom) => classroom.id !== id),
        );
        if (this.classroom$.value.id === id) {
          this.classroom$.next({});
          sessionStorage.removeItem('classroom');
        }
      }),
    );
  }
}
