import { Component, inject, OnInit } from '@angular/core';
import { LegendCardComponent } from "../../../shared/components/legend-card/legend-card.component";
import { MatIconModule } from '@angular/material/icon';
import { StudentGroupsService } from '../../../core/services/student-groups.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentGroup } from '../../../core/models/student-group.interface';

@Component({
  selector: 'app-student-group-view',
  imports: [LegendCardComponent, MatIconModule],
  templateUrl: './student-group-view.component.html',
  styleUrl: './student-group-view.component.scss'
})
export class StudentGroupViewComponent implements OnInit{
    private readonly router = inject(Router);
    private readonly studentGroupService = inject(StudentGroupsService);
    private readonly route = inject(ActivatedRoute);
    student: StudentGroup | undefined;

    ngOnInit(): void {
      this.route.paramMap.subscribe(params => {
        this.studentGroupService.getStudentGroupById(params.get('id')!).subscribe({
          next: (studentGroup) => this.student = studentGroup
        })
    });
    }

    onEdit(id: string){
      this.router.navigate(['/groups/edit',id], {queryParams: {from: 'details'}});
    }
}
