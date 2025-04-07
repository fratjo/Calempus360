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
      }),
    );
  }

  getUniversityById(id: string): Observable<University> {
    return this.http.get<University>(this.URL + `/${id}`);
  }

  setUniversity(id: string) {
    return this.http.get<University>(this.URL + `/${id}`).pipe(
      tap((u: University) => {
        this.university$.next(u);
        sessionStorage.setItem('university', JSON.stringify(u.id));
      }),
    );
  }

  updateUniversity(university: University) {
    return this.http
      .put<University>(this.URL + `/${university.id}`, university)
      .pipe(
        tap((u: University) => {
          const universities = this.universities$.value;
          const index = universities.findIndex((u) => u.id === university.id);
          universities[index] = u;
          this.universities$.next(universities);
          this.university$.next(u);
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

  addUniversity(university: University) {
    return this.http.post<University>(this.URL, university).pipe(
      tap((u: University) => {
        this.universities$.next([...this.universities$.value, u]);
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

  deleteUniversity(id: string) {
    return this.http.delete<University>(this.URL + `/${id}`).pipe(
      tap((u: University) => {
        this.universities$.next(
          this.universities$.value.filter((u) => u.id !== id),
        );
        if (this.university$.value.id === id) {
          this.university$.next({});
          sessionStorage.removeItem('university');
        }
      }),
    );
  }
}
