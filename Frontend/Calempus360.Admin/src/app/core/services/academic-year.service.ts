import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { AcademicYear, AcademicYears } from '../models/academicYear.interface';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AcademicYearService {
  private http: HttpClient = inject(HttpClient);
  private readonly router = inject(Router);
  public academicYear$ = new BehaviorSubject<AcademicYear>({});
  public academicYears$ = new BehaviorSubject<AcademicYears>([]);

  URL = 'http://localhost:5257/api/academic-years';

  isAcademicYear(): boolean {
    return !!this.academicYear$.value && !!this.academicYear$.value.id;
  }

  getAcademicYears() {
    return this.http.get<AcademicYears>(this.URL).pipe(
      tap((a: AcademicYears) => {
        this.academicYears$.next(a);
      })
    );
  }

  getAdademicYearById(id: string) {
    return this.http.get<AcademicYear>(this.URL + `/${id}`).pipe(
      tap((a: AcademicYear) => {
        this.academicYear$.next(a);
        this.router.navigate(['academic-year']);
      })
    );
  }

  addAcademicYear(academicYear: AcademicYear) {
    console.info('Academic Year:', academicYear);
    return this.http.post<AcademicYear>(this.URL, academicYear).pipe(
      tap((a: AcademicYear) => {
        this.academicYear$.next(a);
      })
    );
  }

  updateAcademicYear(academicYear: AcademicYear) {
    console.info('Academic Year:', academicYear);
    return this.http
      .put<AcademicYear>(this.URL + `/${academicYear.id}`, academicYear)
      .pipe(
        tap((a: AcademicYear) => {
          this.academicYear$.next(a);
        })
      );
  }
}
