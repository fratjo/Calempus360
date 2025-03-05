export interface Classroom {
  id?: string;
  name?: string;
  code?: string;
  capacity?: number;
  equipment?: any[];
}

export type Classrooms = Classroom[];
