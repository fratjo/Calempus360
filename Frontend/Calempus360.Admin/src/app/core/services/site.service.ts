import { HttpClient, HttpParams } from '@angular/common/http';
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

  URL = 'http://localhost:5257/api/sites';

  isSite(): boolean {
    return !!this.site$.value && !!this.site$.value.id;
  }

  getSites() {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const params = new HttpParams().set('universityId', universityId);

    return this.http.get<Sites>(this.URL, { params }).pipe(
      tap((s: Sites) => {
        console.log(s);
        this.sites$.next(s);
      }),
    );
  }

  getSiteById(id: string) {
    return this.http.get<Site>(this.URL + `/${id}`).pipe(
      tap((s: Site) => {
        this.site$.next(s);
        sessionStorage.setItem('site', JSON.stringify(s.id));
      }),
    );
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
    return this.http.get<Site>(this.URL + `/${id}`).pipe(
      tap((s: Site) => {
        this.site$.next(s);
        sessionStorage.setItem('site', JSON.stringify(s.id));
      }),
    );
  }

  updateSite(site: Site) {
    return this.http.put<Site>(this.URL + `/${site.id}`, site).pipe(
      tap((s: Site) => {
        const sites = this.sites$.value;
        const index = sites.findIndex((site) => site.id === s.id);
        sites[index] = s;
        this.sites$.next(sites);
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

  addSite(site: Site) {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    const params = new HttpParams().set('universityId', universityId);
    return this.http.post<Site>(this.URL, site, { params }).pipe(
      tap((s: Site) => {
        this.sites$.next([...this.sites$.value, s]);
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

  deleteSite(id: string) {
    return this.http.delete<Site>(this.URL + `/${id}`).pipe(
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
