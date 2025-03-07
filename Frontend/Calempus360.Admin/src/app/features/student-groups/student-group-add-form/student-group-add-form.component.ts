import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { SiteService } from '../../../core/services/site.service';
import { StudentGroupsService } from '../../../core/services/student-groups.service';

@Component({
  selector: 'app-student-group-add-form',
  imports: [ReactiveFormsModule,
      FormsModule,
      MatFormFieldModule,
      MatIconModule,
      RouterModule,
      MatButtonModule,
      CommonModule,],
  templateUrl: './student-group-add-form.component.html',
  styleUrl: './student-group-add-form.component.scss'
})
export class StudentGroupAddFormComponent implements OnInit{
  private readonly router = inject(Router);
  public studentGroupForm: FormGroup;
  formBuilder = inject(FormBuilder);
  private readonly siteService = inject(SiteService);
  private readonly studentGroupService = inject(StudentGroupsService);

  siteList$ = this.siteService.sites$;

  constructor () {
    this.studentGroupForm = this.formBuilder.group({
        code: ['',Validators.required],
        numberOfStudents: ['',Validators.required],
        optionGrade: ['',Validators.required],
        site: [null,Validators.required],
        option:['']
    });
  }

  ngOnInit(): void {
    this.siteService.getSites().subscribe({
      
    });
  }

  onSave(){
    const studentGroup = this.studentGroupForm.value;
    const siteId = this.studentGroupForm.get('site')?.value;
    console.log(siteId);
    const optionId = this.studentGroupForm.get('option')?.value; //Pour test apres faudra changer quand on aura le option
    this.studentGroupService.addStudentGroup(studentGroup,siteId,optionId).subscribe({
      next: (v) => console.log(v),
      error: (e) => {
        const errorMessages = Object.entries(e.error.errors)
        .map(([subject,messages]) =>`${subject}: ${messages}`)
        .join('\n');
        alert(errorMessages);
      },
      complete: () => this.router.navigate(['/groups'])
    });
  }

  onCancel(){
    this.router.navigate(['/groups']);
  }

}
