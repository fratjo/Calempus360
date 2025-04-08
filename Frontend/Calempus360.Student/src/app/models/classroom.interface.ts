export interface Classroom {
  id?: string;
  name?: string;
  code?: string;
  capacity?: number;
  equipment?: any[];
  site?: string;
}

export type Classrooms = Classroom[];
