export interface University {
  id?: string;
  name?: string;
  code?: string;
  address?: string;
  phone?: string;
  createdAt?: string;
  updatedAt?: string;
  sites?: any[];
  equipment?: any[];
}

export type Universities = University[];
