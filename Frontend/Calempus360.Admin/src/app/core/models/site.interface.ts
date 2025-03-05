export interface Site {
  id?: string;
  name?: string;
  code?: string;
  address?: string;
  phone?: string;
  classrooms?: any[];
  schedules?: any[];
  equipment?: any[];
}

export type Sites = Site[];
