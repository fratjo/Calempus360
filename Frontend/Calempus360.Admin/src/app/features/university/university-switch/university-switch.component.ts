import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UniversityService } from '../../../core/services/university.service';
import { AsyncPipe } from '@angular/common';
import { Universities } from '../../../core/models/university.interface';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-university-switch',
  imports: [AsyncPipe],
  templateUrl: './university-switch.component.html',
  styleUrl: './university-switch.component.scss',
})
export class UniversitySwitchComponent implements OnInit {
  private readonly router = inject(Router);
  private universityService = inject(UniversityService);
  universityList$: Observable<Universities> =
    this.universityService.universities$;

  ngOnInit() {
    this.universityService.getUniversities();
    console.log('University List:', this.universityService.universities$);
  }

  onSelect(universityId: string) {
    this.universityService.university$.next(
      this.universityService.universities$.value.find(
        (u) => u.id === universityId
      )!
    );
    this.router.navigate(['/university', universityId]);
  }
}
