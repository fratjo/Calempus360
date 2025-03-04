import { Component, inject, OnInit, signal } from '@angular/core';
import { TopBarComponent } from './layout/top-bar/top-bar.component';
import { UniversityService } from './core/services/university.service';
import { Router, RouterOutlet } from '@angular/router';
import { DockBarComponent } from './layout/dock-bar/dock-bar.component';

@Component({
  selector: 'app-root',
  imports: [TopBarComponent, DockBarComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  private readonly universityService = inject(UniversityService);
  private readonly router = inject(Router);

  ngOnInit() {
    this.router.navigate(['home']);
  }
}
