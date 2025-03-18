import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap, Observable } from 'rxjs';
import { StudentGroup } from '../models/student-group.interface';
import { SiteService } from './site.service';
import { OptionService } from './option.service';

@Injectable({
  providedIn: 'root',
})
export class StudentGroupsService {
  studentGroups$ = new BehaviorSubject<StudentGroup[]>([]);
  private siteService = inject(SiteService);
  private option = inject(OptionService);

  constructor(private http: HttpClient) {}

  URL = 'http://localhost:5257/api/groups';

  getStudentGroups() {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);

    let params = new HttpParams();
    params = params.append('universityId', universityId);
    params = params.append('academicYear', academicYearId);

    const response = this.http
      .get<StudentGroup[]>(this.URL, { params })
      .subscribe({
        next: (groups) => this.studentGroups$.next(groups),
      });
  }

  getStudentGroupById(id: string) {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);

    let params = new HttpParams();
    params = params.append('universityId', universityId);
    params = params.append('academicYear', academicYearId);

    return this.http.get<StudentGroup>(this.URL + `/${id}`, { params });
  }

  addStudentGroup(
    studentGroup: StudentGroup,
    siteId: string,
    optionId: string,
  ) {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);

    let params = new HttpParams();
    params = params.append('universityId', universityId);
    params = params.append('option', optionId);
    params = params.append('site', siteId);

    return this.http.post<StudentGroup>(this.URL, studentGroup, { params });
  }

  updateStudentGroup(
    studentGroup: StudentGroup,
    siteId: string,
    optionId: string,
  ) {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);

    let params = new HttpParams();
    params = params.append('universityId', universityId);
    params = params.append('option', optionId);
    params = params.append('site', siteId);

    return this.http.put<StudentGroup>(this.URL, studentGroup, { params });
  }

  deleteStudentGroup(id: string) {
    return this.http.delete<StudentGroup>(this.URL + `/${id}`);
  }
}
