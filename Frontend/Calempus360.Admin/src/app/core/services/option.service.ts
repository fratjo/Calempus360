import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Option } from '../models/option.interface';
import { StudentGroup } from '../models/student-group.interface';

@Injectable({
  providedIn: 'root',
})
export class OptionService {
  options$ = new BehaviorSubject<Option[]>([]);
  constructor(private http: HttpClient) {}

  URL = 'http://localhost:5257/api/options';

  getOptions() {
    const response = this.http.get<Option[]>(this.URL).subscribe({
      next: (options) => this.options$.next(options),
    });
  }

  getOptionById(id: string) {
    const url = this.URL + `/${id}`;
    return this.http.get<StudentGroup>(url);
  }

  addOption(option: Option) {
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const courses = option.courses
      ?.map((course) => `courses=${course.id}`)
      .join('&');
    const url = this.URL + `?academicYear=${academicYearId}&${courses}`;
    return this.http.post(url, option);
  }

  updateOption(option: Option) {
    const academicYearId = JSON.parse(sessionStorage.getItem('academicYear')!);
    const courses = option.courses
      ?.map((course) => `courses=${course.id}`)
      .join('&');
    const url =
      this.URL + `/${option.id}` + `?academicYear=${academicYearId}&${courses}`;
    return this.http.put<Option>(url, option);
  }

  deleteOption(id: string) {
    const url = this.URL + `/${id}`;
    return this.http.delete<Option>(url);
  }
}
