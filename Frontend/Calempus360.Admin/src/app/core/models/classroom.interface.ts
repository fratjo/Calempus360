export interface Classroom {
  id?: string;
  name?: string;
  code?: string;
  capacity?: number;
  equipment?: any[];
  siteId?: string;
}

export type Classrooms = Classroom[];
