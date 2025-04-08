import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { academicYearGuard } from './academic-year.guard';

describe('academicYearGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => academicYearGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
