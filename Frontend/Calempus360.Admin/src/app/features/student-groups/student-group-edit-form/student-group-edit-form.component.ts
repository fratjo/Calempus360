import { Component, inject, OnInit } from '@angular/core';
import { StudentGroupsService } from '../../../core/services/student-groups.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { StudentGroup } from '../../../core/models/student-group.interface';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { SiteService } from '../../../core/services/site.service';
import { OptionService } from '../../../core/services/option.service';

@Component({
  selector: 'app-student-group-edit-form',
  imports: [ReactiveFormsModule,
        FormsModule,
        MatFormFieldModule,
        MatIconModule,
        RouterModule,
        MatButtonModule,
        CommonModule,],
  templateUrl: './student-group-edit-form.component.html',
  styleUrl: './student-group-edit-form.component.scss'
})
export class StudentGroupEditFormComponent implements OnInit {
  formBuilder = inject(FormBuilder);
  studentGroupForm: FormGroup;
  private readonly siteService = inject(SiteService);
  private readonly studentGroupService = inject(StudentGroupsService);
  private readonly optionService = inject(OptionService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);


  studentGroup: StudentGroup | null = null;

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
    this.route.paramMap.subscribe(params => {
      this.studentGroupService.getStudentGroupById(params.get('id')!).subscribe({
        next: (e) => {
          this.studentGroup = e;
          this.studentGroupForm.patchValue({
            ...this.studentGroup,
            site: this.studentGroup.site?.id,
            option: this.studentGroup.option?.id
          });
        },
        error: (e) => alert("Problem while editing the Group")
      });
    })
    this.siteService.getSites().subscribe();
    this.optionService.getOptions();
  }

  onSave(){
    const studentGroup: StudentGroup = {
      ...this.studentGroupForm.value,
      id: this.studentGroup?.id
    }
    const siteId = this.studentGroupForm.get('site')?.value;
    const optionId = this.studentGroupForm.get('option')?.value;
    this.studentGroupService.updateStudentGroup(studentGroup,siteId,optionId).subscribe({
      next: (v) => console.log(v),
      error: (e) => {
        if(e!=null) {
          const errorMessages = Object.entries(e.error.errors)
        .map(([subject,messages]) =>`${subject}: ${messages}`)
        .join('\n');
        alert(errorMessages);
        } else {
          alert("Problem while editing the Group")
        }
      },
      complete: () => this.goBack()
    });
  }

  onCancel(){
    this.goBack();
  }

  goBack(){
    const origin = this.route.snapshot.queryParamMap.get('from');
    if(origin === 'details') this.router.navigate(['/groups/view',this.studentGroup?.id]);
    else this.router.navigate(['/groups']);
  }

}
