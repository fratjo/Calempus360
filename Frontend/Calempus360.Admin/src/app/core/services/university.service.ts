import { inject, Injectable, signal } from '@angular/core';
import { Universities, University } from '../models/university.interface';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UniversityService {
  private http: HttpClient = inject(HttpClient);

  public university$ = new BehaviorSubject<University>({});
  public universities$ = new BehaviorSubject<University[]>([]);

  URL = 'http://localhost:5257/api/universities';

  ping() {
    return this.http.get(this.URL + '/ping').pipe(
      tap((res) => {
        console.log('Ping Response:', res);
      })
    );
  }

  getUniversities() {
    return this.http
      .get<Universities>(this.URL)
      .pipe(
        tap((u: Universities) => {
          this.universities$.next(u);
          console.log(u);
        })
      )
      .subscribe();
  }

  getUniversityByName(name: string): void {
    // return this.http.get(`https://api.com/universities/${id}`);
  }

  updateUniversity(university: University) {
    return this.http
      .put<University>(this.URL + `/${university.id}`, university)
      .pipe(
        tap((u: University) => {
          this.university$.next(u);
          console.log(this.university$.value);
        })
      )
      .subscribe();
  }

  addUniversity(university: University) {
    return this.http
      .post<University>(this.URL, university)
      .pipe(
        tap((u: University) => {
          this.university$.next(u);
          console.log(this.university$.value);
        })
      )
      .subscribe();
  }
}
