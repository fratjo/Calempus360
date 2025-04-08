import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-legend-card',
  imports: [],
  templateUrl: './legend-card.component.html',
  styleUrl: './legend-card.component.scss'
})
export class LegendCardComponent {
  @Input({required:true}) legend!: string;
}
