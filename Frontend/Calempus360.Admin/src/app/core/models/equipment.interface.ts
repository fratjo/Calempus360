import { Classroom } from './classroom.interface';
import { Site } from './site.interface';

export interface Equipment {
  id?: string;
  name?: string;
  code?: string;
  brand?: string;
  model?: string;
  description?: string;
  equipmentType?: EquipmentType;
  classroom?: Classroom;
  siteId?: string;
}

export type Equipments = Equipment[];

export interface EquipmentType {
  id?: string;
  name?: string;
  code?: string;
  description?: string;
}

export type EquipmentTypes = EquipmentType[];
