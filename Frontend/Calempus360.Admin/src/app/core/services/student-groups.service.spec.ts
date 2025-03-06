import { TestBed } from '@angular/core/testing';

import { StudentGroupsService } from './student-groups.service';

describe('StudentGroupsService', () => {
  let service: StudentGroupsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentGroupsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
