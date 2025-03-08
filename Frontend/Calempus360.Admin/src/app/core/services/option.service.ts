import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Option } from '../models/option.interface';
import { AcademicYearService } from './academic-year.service';
import { StudentGroup } from '../models/student-group.interface';

@Injectable({
  providedIn: 'root'
})
export class OptionService {
  //TODO : Taper le service des cours
  options$ = new BehaviorSubject<Option[]>([]);
  constructor(private http: HttpClient) { }

  URL = 'http://localhost:5257/api/options';


  getOptions(){
    const response = this.http.get<Option[]>(this.URL).subscribe({
      next: (options) => this.options$.next(options)
    });
  }

  getOptionById(id: string){
    const url = this.URL + `/${id}`;
    return this.http.get<StudentGroup>(url);
  }

  addOption(option: Option, courses:string[]){ //TODO : Changer en Course[]
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    //const coursesParam = courses?.map(course => `courses=${course}`).join('&');
    const url = this.URL + `?academicYear=${academicYearId}&${courses}`;
    return this.http.post(url,option);
  }

  updateOption(option: Option){
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    //const courses = option.courses?.map(course => `courses=${course}`).join('&');
    const url = this.URL + `/${option.id}` + `?academicYear=${academicYearId}&${[]}`;
    return this.http.put<Option>(url,option);

  }

  deleteOption(id: string){
    const url = this.URL + `/${id}`;
    return this.http.delete<Option>(url);
  }

}
