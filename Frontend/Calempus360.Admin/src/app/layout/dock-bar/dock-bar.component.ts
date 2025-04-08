import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-dock-bar',
  imports: [MatIconModule, RouterLink, RouterLinkActive],
  templateUrl: './dock-bar.component.html',
  styleUrl: './dock-bar.component.scss',
})
export class DockBarComponent {}
