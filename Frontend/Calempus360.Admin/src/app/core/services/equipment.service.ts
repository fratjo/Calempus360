import { HttpClient, HttpParams } from '@angular/common/http';
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

  URL = 'http://localhost:5257/api/equipments';

  isEquipment(): boolean {
    return !!this.equipment$.value && !!this.equipment$.value.id;
  }

  getEquipmentTypes() {
    return this.http.get<EquipmentTypes>(this.URL + '/types').pipe(
      tap((s: EquipmentTypes) => {
        this.equipmentTypes$.next(s);
      }),
    );
  }

  getEquipmentTypeById(id: string) {
    return this.http.get<EquipmentType>(this.URL + '/types' + `/${id}`).pipe(
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
      .post<EquipmentType>(this.URL + '/types', equipmentType)
      .pipe(
        tap((s: EquipmentType) => {
          this.equipmentTypes$.next([...this.equipmentTypes$.value, s]);
        }),
      );
  }

  updateEquipmentType(equipmentType: EquipmentType) {
    return this.http
      .put<EquipmentType>(
        this.URL + '/types' + `/${equipmentType.id}`,
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
    return this.http.delete<EquipmentType>(this.URL + '/types' + `/${id}`).pipe(
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

  getEquipments({
    universityId,
    siteId,
    classroomId,
    equipmentTypeId,
  }: {
    universityId?: string;
    siteId?: string;
    classroomId?: string;
    equipmentTypeId?: string;
  } = {}) {
    let params = new HttpParams();

    if (universityId) {
      params = params.set('universityId', universityId);
    }
    if (siteId) {
      params = params.set('siteId', siteId);
    }
    if (classroomId) {
      params = params.set('classroomId', classroomId);
    }
    if (equipmentTypeId) {
      params = params.set('equipmentTypeId', equipmentTypeId);
    }

    return this.http.get<Equipments>(this.URL, { params }).pipe(
      tap((s: Equipments) => {
        this.equipments$.next(s);
      }),
    );
  }

  getEquipmentById(id: string) {
    return this.http.get<Equipment>(this.URL + `/${id}`).pipe(
      tap((s: Equipment) => {
        this.equipment$.next(s);
      }),
    );
  }

  setEquipment(id: string) {
    return this.http.get<Equipment>(this.URL + `/${id}`).pipe(
      tap((s: Equipment) => {
        this.equipment$.next(s);
        sessionStorage.setItem('equipment', JSON.stringify(s.id));
      }),
    );
  }

  addEquipment(equipment: Equipment) {
    let params = new HttpParams()
      .set('universityId', JSON.parse(sessionStorage.getItem('university')!))
      .set('siteId', equipment.siteId!);

    return this.http.post<Equipment>(this.URL, equipment, { params }).pipe(
      tap((s: Equipment) => {
        this.equipments$.next([...this.equipments$.value, s]);
      }),
    );
  }

  updateEquipment(equipment: Equipment) {
    let params = new HttpParams().set(
      'academicYearId',
      JSON.parse(sessionStorage.getItem('academicYear')!),
    );

    return this.http
      .put<Equipment>(this.URL + `/${equipment.id}`, equipment, { params })
      .pipe(
        tap((s: Equipment) => {
          this.equipments$.next(
            this.equipments$.value.map((e) => (e.id === s.id ? s : e)),
          );
          this.equipment$.next(s);
        }),
      );
  }

  deleteEquipment(id: string) {
    return this.http.delete<Equipment>(this.URL + `/${id}`).pipe(
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
