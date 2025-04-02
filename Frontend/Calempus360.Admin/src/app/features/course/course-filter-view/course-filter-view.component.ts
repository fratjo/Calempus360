import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, inject, OnInit, output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { LegendCardComponent } from '../../../shared/components/legend-card/legend-card.component';
import { EquipmentService } from '../../../core/services/equipment.service';

@Component({
  selector: 'app-course-filter-view',
  imports: [AsyncPipe, MatIconModule, MatButtonModule, CommonModule, LegendCardComponent],
  templateUrl: './course-filter-view.component.html',
  styleUrl: './course-filter-view.component.scss'
})
export class CourseFilterViewComponent implements OnInit{
  
  private readonly equipmentTypeService = inject(EquipmentService);
  equipmentTypeList$ = this.equipmentTypeService.equipmentTypes$;
  equipmentTypeId = output<any>();

  ngOnInit(): void {
    this.equipmentTypeService.getEquipmentTypes().subscribe();
  }

  onEquipmentTypeChange(event: any){
    this.equipmentTypeId.emit(event.target.value);
  }
}
