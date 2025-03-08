import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';
import { OptionService } from '../../core/services/option.service';
import { Observable } from 'rxjs';
import { Option } from '../../core/models/option.interface';

@Component({
  selector: 'app-option',
  imports: [RouterLink, AsyncPipe, MatIconModule, MatButtonModule],
  templateUrl: './option.component.html',
  styleUrl: './option.component.scss'
})
export class OptionComponent {
  private readonly router = inject(Router);
  private readonly optionService = inject(OptionService);

  options$: Observable<Option[]> = this.optionService.options$;

  ngOnInit(): void {
    this.updateUI();
  }

  updateUI(){
    this.optionService.getOptions();
  }

  onEdit(id: string){
    this.router.navigate(['/options/edit',id]);
  }

  onDelete(id: string){
    this.optionService.deleteOption(id).subscribe({
      next: () => {
        this.updateUI();
      }
    }); 
  }

}
