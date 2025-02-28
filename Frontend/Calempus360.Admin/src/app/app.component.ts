import { Component, inject, OnInit, signal } from '@angular/core';
import { TopBarComponent } from './layout/top-bar/top-bar.component';
import { LeftPanelComponent } from './layout/left-panel/left-panel.component';
import { UniversityService } from './core/services/university.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-root',
  imports: [TopBarComponent, LeftPanelComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  private readonly universityService = inject(UniversityService);
  private readonly dialog = inject(MatDialog);

  constructor() {}

  ngOnInit() {}
}
