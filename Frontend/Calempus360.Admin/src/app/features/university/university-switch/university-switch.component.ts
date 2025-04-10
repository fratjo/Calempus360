import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UniversityService } from '../../../core/services/university.service';
import { AsyncPipe } from '@angular/common';
import { Universities } from '../../../core/models/university.interface';
import { Observable } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-university-switch',
  imports: [AsyncPipe, MatButtonModule, MatIconModule, RouterLink],
  templateUrl: './university-switch.component.html',
  styleUrl: './university-switch.component.scss',
})
export class UniversitySwitchComponent implements OnInit {
  private readonly router = inject(Router);
  private universityService = inject(UniversityService);
  universityList$: Observable<Universities> =
    this.universityService.universities$;

  ngOnInit() {
    this.universityService.getUniversities().subscribe();
  }

  onSelect(universityId: string) {
    this.universityService.setUniversity(universityId).subscribe();
    this.router.navigate(['']);
  }

  onEdit(universityId: string) {
    this.router.navigate(['/university/edit', universityId]);
  }

  onDelete(universityId: string) {
    this.universityService.deleteUniversity(universityId).subscribe();
  }
}
