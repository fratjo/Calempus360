import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { SiteService } from '../../../core/services/site.service';
import { StudentGroupsService } from '../../../core/services/student-groups.service';
import { OptionService } from '../../../core/services/option.service';

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
  studentGroupForm: FormGroup;
  formBuilder = inject(FormBuilder);
  private readonly siteService = inject(SiteService);
  private readonly studentGroupService = inject(StudentGroupsService);
  private readonly optionService = inject(OptionService);

  siteList$ = this.siteService.sites$;
  optionList$ = this.optionService.options$;

  constructor () {
    this.studentGroupForm = this.formBuilder.group({
        code: ['',Validators.required],
        numberOfStudents: ['',Validators.required],
        optionGrade: ['',Validators.required],
        site: [null,Validators.required],
        option:[null,Validators.required]
    });
  }

  ngOnInit(): void {
    this.siteService.getSites().subscribe({});
    this.optionService.getOptions();
  }

  onSave(){
    const studentGroup = this.studentGroupForm.value;
    const siteId = this.studentGroupForm.get('site')?.value;
    const optionId = this.studentGroupForm.get('option')?.value;
    this.studentGroupService.addStudentGroup(studentGroup,siteId,optionId).subscribe({
      next: (v) => console.log(v),
      error: (e) => {
        const errorMessages = Object.entries(e.error.errors);
        if(errorMessages){
          errorMessages.map(([subject,messages]) =>`${subject}: ${messages}`)
        .join('\n');
        alert(errorMessages);
        } else {
          alert("Error when adding a group !");
        }
        
      },
      complete: () => this.router.navigate(['/groups'])
    });
  }

  onCancel(){
    this.router.navigate(['/groups']);
  }

}
