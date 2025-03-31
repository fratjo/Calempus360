import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Session, Sessions } from '../models/session.interface';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  public sessions$ = new BehaviorSubject<Sessions>([]);

  constructor(private http: HttpClient) {}

  URL = 'http://localhost:5257/api/sessions';

  getSessionById(id: string): Observable<Session> {
    return this.http.get<Session>(`${this.URL}/${id}`);
  }

  getSessions({
    universityId,
    academicYearId,
    studentGroupId,
    classroomId,
    courseId,
  }: {
    universityId?: string;
    academicYearId?: string;
    studentGroupId?: string;
    classroomId?: string;
    courseId?: string;
  }): Observable<Sessions> {
    let params = new HttpParams();

    if (universityId) {
      params = params.append('universityId', universityId);
    }
    if (academicYearId) {
      params = params.append('academicYearId', academicYearId);
    }
    if (studentGroupId) {
      params = params.append('studentGroupId', studentGroupId);
    }
    if (classroomId) {
      params = params.append('classroomId', classroomId);
    }
    if (courseId) {
      params = params.append('courseId', courseId);
    }

    return this.http.get<Sessions>(this.URL, { params }).pipe(
      tap((sessions: Sessions) => {
        this.sessions$.next(sessions);
      }),
    );
  }

  updateSessions(session: Session) {
    return this.http.put<Session>(`${this.URL}/${session.id}`, session);
  }

  generateSessions({
    universityId,
    academicYearId,
  }: {
    universityId: string;
    academicYearId: string;
  }) {
    let params = new HttpParams();
    if (universityId) {
      params = params.append('universityId', universityId);
    }
    if (academicYearId) {
      params = params.append('academicYearId', academicYearId);
    }

    return this.http.post(`${this.URL}/generate`, {}, { params });
  }
}
