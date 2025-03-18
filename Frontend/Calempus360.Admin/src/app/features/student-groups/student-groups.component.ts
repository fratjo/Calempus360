import { Component, inject, OnInit } from '@angular/core';
import { StudentGroupsService } from '../../core/services/student-groups.service';
import { Router, RouterLink } from '@angular/router';
import { StudentGroup } from '../../core/models/student-group.interface';
import { map, Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { LegendCardComponent } from "../../shared/components/legend-card/legend-card.component";
import { SiteService } from '../../core/services/site.service';
import { Site } from '../../core/models/site.interface';
import { OptionService } from '../../core/services/option.service';
import { Option } from '../../core/models/option.interface';

@Component({
  selector: 'app-student-groups',
  imports: [RouterLink, AsyncPipe, MatIconModule, MatButtonModule, LegendCardComponent],
  templateUrl: './student-groups.component.html',
  styleUrl: './student-groups.component.scss'
})
export class StudentGroupsComponent implements OnInit{

  private readonly router = inject(Router);
  private readonly studentGroupService = inject(StudentGroupsService);
  private readonly siteService = inject(SiteService);
  private readonly optionService = inject(OptionService);

  studentGroups$: Observable<StudentGroup[]> = this.studentGroupService.studentGroups$;
  sites$: Observable<Site[]> = this.siteService.sites$;
  options$: Observable<Option[]> = this.optionService.options$;

  private siteFilter: any = 0;
  private optionFilter: any = 0;


  ngOnInit(): void {
    this.updateUI();
  }

  onSelect(id: string){
    this.router.navigate(['/groups/view',id]);
  }

  onEdit(id: string){
    this.router.navigate(['/groups/edit',id]);
  }

  onDelete(id: string){
    this.studentGroupService.deleteStudentGroup(id).subscribe({
      next: () => {
        this.updateUI();
      }
    }); 
  }
  
  updateUI(){
    this.siteService.getSites().subscribe();
    this.optionService.getOptions();
    this.studentGroupService.getStudentGroups();
  }

  onSiteChange(event: any){
    this.siteFilter = event.target.value;
    console.log(this.siteFilter);
    this.updateStudentList();
  }

  onOptionChange(event: any){
    this.optionFilter = event.target.value;
    this.updateStudentList();
  }

  updateStudentList(){
    if(this.optionFilter != 0 || this.siteFilter != 0){
      this.studentGroups$ = this.studentGroups$.pipe(
        map((studentGroups: StudentGroup[]) => studentGroups.filter(studentGroup => 
          (this.optionFilter == 0 || studentGroup.option!.id == this.optionFilter) &&
          (this.siteFilter == 0 || studentGroup.site!.id == this.siteFilter)
        ))
      );
    } else {
      this.studentGroups$ = this.studentGroupService.studentGroups$;
    }
  }
}
