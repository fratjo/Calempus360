import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap,Observable } from 'rxjs';
import { StudentGroup } from '../models/student-group.interface';
import { SiteService } from './site.service';
import { OptionService } from './option.service';

@Injectable({
  providedIn: 'root'
})
export class StudentGroupsService {

  studentGroups$ = new BehaviorSubject<StudentGroup[]>([]);
  private siteService = inject(SiteService);
  private option = inject(OptionService);
  
  constructor(private http: HttpClient) { }

  URL = 'http://localhost:5257/api/university/{universityId}/groups';


  
  getStudentGroups(){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const url = this.URL.replace('{universityId}', universityId) + `?academicYear=${academicYearId}`;
    console.log(url);
    const response = this.http.get<StudentGroup[]>(url).subscribe({
      next: (groups) => this.studentGroups$.next(groups),
    });
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
    const url = this.URL.replace('{universityId}', universityId) + `/${studentGroup.id}` +`?option=${optionId}&site=${siteId}`;
    return this.http.put<StudentGroup>(url,studentGroup);
  }

  deleteStudentGroup(id: string){
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const url = this.URL.replace('{universityId}', universityId) + `/${id}`;
    return this.http.delete<StudentGroup>(url);
  }


}
