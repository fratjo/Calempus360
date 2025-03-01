import { inject, Injectable, signal } from '@angular/core';
import { Universities, University } from '../models/university.interface';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class UniversityService {
  private http: HttpClient = inject(HttpClient);
  private readonly router = inject(Router);
  public university$ = new BehaviorSubject<University>({});
  public universities$ = new BehaviorSubject<University[]>([]);

  URL = 'http://localhost:5257/api/universities';

  isUniversity(): boolean {
    return !!this.university$.value && !!this.university$.value.id;
  }

  getUniversities() {
    return this.http.get<Universities>(this.URL).pipe(
      tap((u: Universities) => {
        this.universities$.next(u);
      })
    );
  }

  getUniversityById(id: string) {
    return this.http.get<University>(this.URL + `/${id}`).pipe(
      tap((u: University) => {
        this.university$.next(u);
        this.router.navigate(['university']);
      })
    );
  }

  updateUniversity(university: University) {
    return this.http
      .put<University>(this.URL + `/${university.id}`, university)
      .pipe(
        tap((u: University) => {
          this.university$.next(u);
        })
      );
  }

  addUniversity(university: University) {
    return this.http.post<University>(this.URL, university).pipe(
      tap((u: University) => {
        this.university$.next(u);
      })
    );
  }
}
