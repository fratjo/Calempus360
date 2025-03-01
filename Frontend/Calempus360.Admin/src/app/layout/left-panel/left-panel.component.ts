import { Component, inject } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { UniversityService } from '../../core/services/university.service';
import { MatInputModule } from '@angular/material/input';
import { AcademicYearService } from '../../core/services/academic-year.service';

@Component({
  selector: 'app-left-panel',
  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatSidenavModule,
    MatIconModule,
    MatInputModule,
  ],
  templateUrl: './left-panel.component.html',
  styleUrl: './left-panel.component.scss',
})
export class LeftPanelComponent {
  private universityService = inject(UniversityService);
  private academicYearService = inject(AcademicYearService);
}
