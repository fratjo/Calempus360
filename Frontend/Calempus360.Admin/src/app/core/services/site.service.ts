import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Site, Sites } from '../models/site.interface';
import { UniversityService } from './university.service';

@Injectable({
  providedIn: 'root',
})
export class SiteService {
  private http: HttpClient = inject(HttpClient);
  private universityService = inject(UniversityService);
  public site$ = new BehaviorSubject<Site>({});
  public sites$ = new BehaviorSubject<Sites>([]);

  URL = 'http://localhost:5257/api/university/{universityId}/sites';

  isSite(): boolean {
    return !!this.site$.value && !!this.site$.value.id;
  }

  getSites() {
    const universityId = this.universityService.university$.value.id;
    const url = this.URL.replace('{universityId}', universityId!);
    console.log(url);

    return this.http.get<Sites>(url).pipe(
      tap((s: Sites) => {
        this.sites$.next(s);
      }),
    );
  }

  getSiteById(id: string) {
    const universityId = this.universityService.university$.value.id;
    const url = this.URL.replace('{universityId}', universityId!);
    return this.http.get<Site>(url + `/${id}`);
    // .pipe(
    //   tap((s: Site) => {
    //     this.site$.next(s);
    //   }),
    // );
  }

  setSite(id: string) {
    const universityId = this.universityService.university$.value.id;
    const url = this.URL.replace('{universityId}', universityId!);
    return this.http.get<Site>(url + `/${id}`).pipe(
      tap((s: Site) => {
        this.site$.next(s);
      }),
    );
  }

  updateSite(site: Site) {
    const universityId = this.universityService.university$.value.id;
    const url = this.URL.replace('{universityId}', universityId!);
    return this.http.put<Site>(url + `/${site.id}`, site);
    // .pipe(
    //   tap((s: Site) => {
    //     this.site$.next(s);
    //   }),
    // );
  }

  addSite(site: Site) {
    const universityId = this.universityService.university$.value.id;
    const url = this.URL.replace('{universityId}', universityId!);
    return this.http.post<Site>(url, site);
    // .pipe(
    //   tap((s: Site) => {
    //     this.site$.next(s);
    //   }),
    // );
  }
}
