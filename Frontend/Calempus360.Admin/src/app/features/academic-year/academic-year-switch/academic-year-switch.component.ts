import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {
  RouterLink,
  RouterOutlet,
  RouterLinkActive,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AcademicYearService } from '../../../core/services/academic-year.service';
import {
  AcademicYear,
  AcademicYears,
} from '../../../core/models/academicYear.interface';

@Component({
  selector: 'app-academic-year-switch',
  imports: [AsyncPipe, MatButtonModule, MatIconModule, RouterLink],
  templateUrl: './academic-year-switch.component.html',
  styleUrl: './academic-year-switch.component.scss',
})
export class AcademicYearSwitchComponent implements OnInit {
  private readonly router = inject(Router);
  private academicYearService = inject(AcademicYearService);
  academicYearList$: Observable<AcademicYears> =
    this.academicYearService.academicYears$;

  ngOnInit() {
    this.academicYearService.getAcademicYears().subscribe();
    console.log('Academic Year List:', this.academicYearService.academicYear$);
  }

  onSelect(academicYearId: string) {
    this.academicYearService.setAcademicYear(academicYearId).subscribe();
    this.router.navigate(['']);
  }

  onEdit(academicYearId: string) {
    this.router.navigate(['/academic-year/edit', academicYearId]);
  }

  onDelete(academicYearId: string) {
    this.academicYearService.deleteAcademicYear(academicYearId).subscribe();
  }
}
