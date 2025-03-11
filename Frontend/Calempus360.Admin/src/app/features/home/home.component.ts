import { Component, inject } from '@angular/core';
import { UniversityService } from '../../core/services/university.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  private readonly universityService = inject(UniversityService);
  public university$ = this.universityService.university$;
}
