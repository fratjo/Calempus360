import { Component, inject, input, Input } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { UniversityService } from '../../core/services/university.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-top-bar',
  imports: [MatToolbarModule, AsyncPipe],
  templateUrl: './top-bar.component.html',
  styleUrl: './top-bar.component.scss',
})
export class TopBarComponent {}
