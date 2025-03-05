import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UniversityService } from '../services/university.service';

export const univeristyGuard: CanActivateFn = () => {
  const universityService = inject(UniversityService);
  const route = inject(Router);

  if (!universityService.isUniversity()) {
    console.log('No university found, redirecting to university change page');
    route.navigate(['/university/change']);
    return false;
  }

  return true;
};
