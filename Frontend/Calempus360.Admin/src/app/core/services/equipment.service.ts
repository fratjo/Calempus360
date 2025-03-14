import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import {
  Equipment,
  Equipments,
  EquipmentType,
  EquipmentTypes,
} from '../models/equipment.interface';
import { SiteService } from './site.service';
import { UniversityService } from './university.service';

@Injectable({
  providedIn: 'root',
})
export class EquipmentService {
  private http: HttpClient = inject(HttpClient);
  private universityService = inject(UniversityService);
  private siteService = inject(SiteService);
  public equipment$ = new BehaviorSubject<Equipment>({});
  public equipments$ = new BehaviorSubject<Equipments>([]);
  public equipmentType$ = new BehaviorSubject<Equipment>({});
  public equipmentTypes$ = new BehaviorSubject<Equipments>([]);

  URL = '/api/equipments';
  URl_BASE = 'http://localhost:5257';

  isEquipment(): boolean {
    return !!this.equipment$.value && !!this.equipment$.value.id;
  }

  getEquipmentTypes() {
    return this.http
      .get<EquipmentTypes>(this.URl_BASE + this.URL + '/equipment-types')
      .pipe(
        tap((s: EquipmentTypes) => {
          this.equipmentTypes$.next(s);
        }),
      );
  }

  getEquipmentTypeById(id: string) {
    return this.http
      .get<EquipmentType>(
        this.URl_BASE + this.URL + '/equipment-types' + `/${id}`,
      )
      .pipe(
        tap((s: EquipmentType) => {
          this.equipmentType$.next(s);
        }),
      );
  }

  setEquipmentType(id: string) {
    return this.getEquipmentTypeById(id).pipe(
      tap((s: EquipmentType) => {
        this.equipmentType$.next(s);
        sessionStorage.setItem('equipmentType', JSON.stringify(s.id));
      }),
    );
  }

  addEquipmentType(equipmentType: EquipmentType) {
    return this.http
      .post<EquipmentType>(
        this.URl_BASE + this.URL + '/equipment-types',
        equipmentType,
      )
      .pipe(
        tap((s: EquipmentType) => {
          this.equipmentTypes$.next([...this.equipmentTypes$.value, s]);
        }),
      );
  }

  updateEquipmentType(equipmentType: EquipmentType) {
    return this.http
      .put<EquipmentType>(
        this.URl_BASE + this.URL + '/equipment-types' + `/${equipmentType.id}`,
        equipmentType,
      )
      .pipe(
        tap((s: EquipmentType) => {
          this.equipmentTypes$.next(
            this.equipmentTypes$.value.map((e) => (e.id === s.id ? s : e)),
          );
        }),
      );
  }

  deleteEquipmentType(id: string) {
    return this.http
      .delete<EquipmentType>(
        this.URl_BASE + this.URL + '/equipment-types' + `/${id}`,
      )
      .pipe(
        tap(() => {
          this.equipmentTypes$.next(
            this.equipmentTypes$.value.filter((e) => e.id !== id),
          );
          if (this.equipmentType$.value.id === id) {
            this.equipmentType$.next({});
            sessionStorage.removeItem('equipmentType');
          }
        }),
      );
  }

  // Equipment //

  getEquipmentsByUniversity() {
    const url = '/api/equipments/universities/{universityId}';

    const parsedUrl = url.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );

    return this.http.get<Equipments>(this.URl_BASE + parsedUrl).pipe(
      tap((s: Equipments) => {
        this.equipments$.next(s);
      }),
    );
  }

  getEquipmentsBySite(id: string | null = null) {
    const url = '/api/equipments/sites/{siteId}';

    const parsedUrl = url.replace(
      '{siteId}',
      id === null ? JSON.parse(sessionStorage.getItem('site')!) : id,
    );

    return this.http.get<Equipments>(this.URl_BASE + parsedUrl).pipe(
      tap((s: Equipments) => {
        this.equipments$.next(s);
      }),
    );
  }

  getEquipmentsByClassroom(id: string | null = null) {
    const classroomId =
      id === null ? JSON.parse(sessionStorage.getItem('classroom')!) : id;

    return this.http
      .get<Equipments>(`${this.URl_BASE + this.URL}?classroomId=${classroomId}`)
      .pipe(
        tap((s: Equipments) => {
          this.equipments$.next(s);
        }),
      );
  }

  getEquipmentsByEquipmentType(id: string | null = null) {
    const url = '/api/equipments/equipment-types/{equipmentTypeId}';
    const parsedUrl = url.replace(
      '{equipmentTypeId}',
      id === null ? JSON.parse(sessionStorage.getItem('equipmentType')!) : id,
    );

    return this.http.get<Equipments>(this.URl_BASE + parsedUrl).pipe(
      tap((s: Equipments) => {
        this.equipments$.next(s);
      }),
    );
  }

  getEquipmentById(id: string) {
    return this.http.get<Equipment>(this.URl_BASE + this.URL + `/${id}`).pipe(
      tap((s: Equipment) => {
        this.equipment$.next(s);
      }),
    );
  }

  setEquipment(id: string) {
    return this.http.get<Equipment>(this.URl_BASE + this.URL + `/${id}`).pipe(
      tap((s: Equipment) => {
        this.equipment$.next(s);
        sessionStorage.setItem('equipment', JSON.stringify(s.id));
      }),
    );
  }

  addEquipment(equipment: Equipment) {
    const url = '/api/equipments/universities/{universityId}';

    const parsedUrl = url.replace(
      '{universityId}',
      JSON.parse(sessionStorage.getItem('university')!),
    );

    return this.http.post<Equipment>(this.URl_BASE + parsedUrl, equipment).pipe(
      tap((s: Equipment) => {
        this.equipments$.next([...this.equipments$.value, s]);
      }),
    );
  }

  updateEquipment(equipment: Equipment) {
    return this.http
      .put<Equipment>(this.URl_BASE + this.URL + `/${equipment.id}`, equipment)
      .pipe(
        tap((s: Equipment) => {
          this.equipments$.next(
            this.equipments$.value.map((e) => (e.id === s.id ? s : e)),
          );
        }),
      );
  }

  deleteEquipment(id: string) {
    return this.http
      .delete<Equipment>(this.URl_BASE + this.URL + `/${id}`)
      .pipe(
        tap(() => {
          this.equipments$.next(
            this.equipments$.value.filter((e) => e.id !== id),
          );
          if (this.equipment$.value.id === id) {
            this.equipment$.next({});
            sessionStorage.removeItem('equipment');
          }
        }),
      );
  }
}
