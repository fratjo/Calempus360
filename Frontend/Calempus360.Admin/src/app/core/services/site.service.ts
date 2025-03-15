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

  URL = 'http://localhost:5257/api/universities/{universityId}/sites';

  isSite(): boolean {
    return !!this.site$.value && !!this.site$.value.id;
  }

  getSites() {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );

    return this.http.get<Sites>(url).pipe(
      tap((s: Sites) => {
        this.sites$.next(s);
      }),
    );
  }

  getSiteById(id: string) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    return this.http.get<Site>(url + `/${id}`);
  }

  getSiteByEquipmentId(id: string) {
    const url =
      'http://localhost:5257/api/equipments/{equipmentId}/site'.replace(
        '{equipmentId}',
        id,
      );

    return this.http.get<Site>(url).pipe(
      tap({
        next: (s: Site) => {
          console.log(s);
          this.site$.next(s);
        },
        error: (err) => {
          console.error('Error fetching site by equipment ID:', err);
        },
      }),
    );
  }

  setSite(id: string) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    return this.http.get<Site>(url + `/${id}`).pipe(
      tap((s: Site) => {
        this.site$.next(s);
        sessionStorage.setItem('site', JSON.stringify(s.id));
      }),
    );
  }

  updateSite(site: Site) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    return this.http.put<Site>(url + `/${site.id}`, site).pipe(
      tap((s: Site) => {
        const sites = this.sites$.value;
        const index = sites.findIndex((site) => site.id === s.id);
        sites[index] = s;
        this.sites$.next(sites);
      }),
    );
  }

  addSite(site: Site) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    return this.http.post<Site>(url, site).pipe(
      tap((s: Site) => {
        this.sites$.next([...this.sites$.value, s]);
      }),
    );
  }

  deleteSite(id: string) {
    const url = this.URL.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );
    return this.http.delete<Site>(url + `/${id}`).pipe(
      tap((s: Site) => {
        this.sites$.next(this.sites$.value.filter((site) => site.id !== id));
        if (this.site$.value.id === id) {
          this.site$.next({});
          sessionStorage.removeItem('site');
        }
      }),
    );
  }
}
