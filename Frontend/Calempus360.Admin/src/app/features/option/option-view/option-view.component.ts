import { Component, inject, OnInit } from '@angular/core';
import { Option } from '../../../core/models/option.interface';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { LegendCardComponent } from '../../../shared/components/legend-card/legend-card.component';
import { OptionService } from '../../../core/services/option.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-option-view',
  imports: [CommonModule, MatIconModule,LegendCardComponent],
  templateUrl: './option-view.component.html',
  styleUrl: './option-view.component.scss'
})
export class OptionViewComponent implements OnInit{
  
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly optionService = inject(OptionService);
  option: Option | undefined;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.optionService.getOptionById(params.get('id')!).subscribe({
        next: (option) => this.option = option
      })
  });
  }

  onEdit(id: string){
    this.router.navigate(['/options/edit',id], {queryParams: {from: 'details'}});
  }
}
