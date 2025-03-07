import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap,Observable } from 'rxjs';
import { StudentGroup } from '../models/student-group.interface';
import { UniversityService } from './university.service';
import { AcademicYearService } from './academic-year.service';
import { SiteService } from './site.service';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupsService {
  private studentGroup$ = new BehaviorSubject<StudentGroup>({});
  studentGroups$ = new BehaviorSubject<StudentGroup[]>([]);
  private universityService = inject(UniversityService);
  private academicYearService = inject(AcademicYearService);
  private siteService = inject(SiteService);
  //private option = inject(OptionService);
  constructor(private http: HttpClient) { }

  URL = 'http://localhost:5257/api/university/{universityId}/groups';

  isStudentGroup(): boolean {
    return !!this.studentGroup$.value && !!this.studentGroup$.value.id;
  }
  //Changer Avec method du get
  getStudentGroups(){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const url = this.URL.replace('{universityId}', universityId) + `?academicYear=${academicYearId}`;
    console.log(url);
    const response = this.http.get<StudentGroup[]>(url).subscribe({
      next: (groups) => this.studentGroups$.next(groups),
    });
    //console.log(response.closed);
  }

  getStudentGroupById(id: string){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const url = this.URL.replace('{universityId}', universityId) + `/${id}` +`?academicYear=${academicYearId}`;
    return this.http.get<StudentGroup>(url);
  }

  addStudentGroup(studentGroup: StudentGroup,siteId: string, optionId: string){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const url = this.URL.replace('{universityId}', universityId) +`?option=${optionId}&site=${siteId}&academicYear=${academicYearId}`;
    return this.http.post<StudentGroup>(url,studentGroup);
  }

  updateStudentGroup(studentGroup: StudentGroup, siteId: string, optionId: string){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const url = this.URL.replace('{universityId}', universityId) + `/${studentGroup.id}` +`?option=${optionId}?site=${siteId}`;
    return this.http.put<StudentGroup>(url,studentGroup);
  }

  deleteStudentGroup(id: string){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const url = this.URL.replace('{universityId}', universityId) + `/${id}`;
    return this.http.delete<StudentGroup>(url);
  }


}
