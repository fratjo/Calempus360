import { DateInput } from '@fullcalendar/core/index.js';
import { Classroom } from './classroom.interface';
import { Course } from './course.interface';
import { Equipment, Equipments } from './equipment.interface';
import { StudentGroup } from './student-group.interface';

export interface Session {
  id?: string;
  name?: string;
  dateTimeStart?: DateInput;
  dateTimeEnd?: DateInput;
  classroom?: any;
  course?: any;
  studentGroups?: any[];
  equipments?: any[];
}

export type Sessions = Session[];
