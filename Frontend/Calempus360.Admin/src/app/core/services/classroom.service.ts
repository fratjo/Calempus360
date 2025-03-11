import { inject, Injectable } from '@angular/core';
import { Classroom, Classrooms } from '../models/classroom.interface';
import { HttpClient } from '@angular/common/http';
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

  URL =
    'http://localhost:5257/api/universities/{universityId}/sites/{siteId}/classrooms';

  isClassroom(): boolean {
    return !!this.classroom$.value && !!this.classroom$.value.id;
  }

  getClassroomsByUniversity() {
    const url =
      'http://localhost:5257/api/universities/{universityId}/classrooms'.replace(
        '{universityId}',
        JSON.parse(sessionStorage.getItem('university')!),
      );

    return this.http.get<Classrooms>(url).pipe(
      tap((s: Classrooms) => {
        this.classrooms$.next(s);
      }),
    );
  }

  getClassroomsBySite(siteId: string | null = null) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );

    const siteUrl = url.replace(
      '{siteId}',
      siteId === null ? JSON.parse(sessionStorage.getItem('site')!) : siteId,
    );

    console.log(siteUrl);

    return this.http.get<Classrooms>(siteUrl).pipe(
      tap((s: Classrooms) => {
        this.classrooms$.next(s);
      }),
    );
  }

  getClassroomById(id: string) {
    const url = 'http://localhost:5257/api/classrooms';

    return this.http.get<Classroom>(url + `/${id}`);
  }

  setClassroom(id: string) {
    const url = 'http://localhost:5257/api/classrooms';

    return this.http.get<Classroom>(url + `/${id}`).pipe(
      tap((s: Classroom) => {
        this.classroom$.next(s);
        sessionStorage.setItem('classroom', JSON.stringify(s.id));
      }),
    );
  }

  updateClassroom(classroom: Classroom) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    const siteUrl = url.replace(
      '{siteId}',
      JSON.parse(sessionStorage.getItem('site')!),
    );

    return this.http
      .put<Classroom>(siteUrl + `/${classroom.id}`, classroom)
      .pipe(
        tap((s: Classroom) => {
          const sites = this.classrooms$.value;
          const index = sites.findIndex((classroom) => classroom.id === s.id);
          sites[index] = s;
          this.classrooms$.next(sites);
        }),
      );
  }

  addClassroom(classroom: Classroom) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    const siteUrl = url.replace('{siteId}', classroom.siteId!);

    return this.http
      .post<Classroom>(siteUrl, {
        name: classroom.name,
        code: classroom.code,
        capacity: Number(classroom.capacity),
      })
      .pipe(
        tap((s: Classroom) => {
          this.classrooms$.next([...this.classrooms$.value, s]);
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
