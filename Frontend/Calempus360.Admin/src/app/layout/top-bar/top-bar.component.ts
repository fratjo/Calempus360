import {
  Component,
  inject,
  input,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { UniversityService } from '../../core/services/university.service';
import { AsyncPipe } from '@angular/common';
import { AcademicYearService } from '../../core/services/academic-year.service';

@Component({
  selector: 'app-top-bar',
  imports: [MatToolbarModule, AsyncPipe],
  templateUrl: './top-bar.component.html',
  styleUrl: './top-bar.component.scss',
})
export class TopBarComponent {
  university$ = inject(UniversityService).university$.asObservable();
  academicYear$ = inject(AcademicYearService).academicYear$.asObservable();
}
