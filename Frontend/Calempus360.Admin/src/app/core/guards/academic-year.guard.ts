import { CanActivateFn, Router } from '@angular/router';
import { AcademicYearService } from '../services/academic-year.service';
import { inject } from '@angular/core';

export const academicYearGuard: CanActivateFn = () => {
  const academicYearService = inject(AcademicYearService);
  const route = inject(Router);

  if (
    !academicYearService.isAcademicYear() &&
    sessionStorage.getItem('academicYear') === null
  ) {
    console.log(
      'No academic year found, redirecting to academic year change page',
    );
    route.navigate(['schedules']);
    return false;
  }

  return true;
};
