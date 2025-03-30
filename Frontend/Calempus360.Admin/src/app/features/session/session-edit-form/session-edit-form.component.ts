import { Component, inject, OnInit } from '@angular/core';
import { StudentGroupsService } from '../../../core/services/student-groups.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CourseService } from '../../../core/services/course.service';
import { EquipmentService } from '../../../core/services/equipment.service';
import { ClassroomService } from '../../../core/services/classroom.service';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseFormCustomInputComponent } from '../../course/course-form-custom-input/course-form-custom-input.component';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Equipment } from '../../../core/models/equipment.interface';
import { StudentGroup } from '../../../core/models/student-group.interface';
import { SessionService } from '../../../core/services/session.service';
import { Session } from '../../../core/models/session.interface';

@Component({
  selector: 'app-session-edit-form',
  imports: [ReactiveFormsModule,
              FormsModule,
              MatFormFieldModule,
              MatIconModule,
              RouterModule,
              MatButtonModule,
              CommonModule],
  templateUrl: './session-edit-form.component.html',
  styleUrl: './session-edit-form.component.scss'
})
export class SessionEditFormComponent implements OnInit{
  private readonly courseService = inject(CourseService);
  private readonly equipmentService = inject(EquipmentService);
  private readonly studentGroupService = inject(StudentGroupsService);
  private readonly classRoomService = inject(ClassroomService);
  private readonly sessionService = inject(SessionService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  formBuilder = inject(FormBuilder);
  sessionForm: FormGroup;
  session: Session | undefined;
  classrooms$ = this.classRoomService.classrooms$;
  courses$ = this.courseService.courses$;
  studentGroups$ = this.studentGroupService.studentGroups$;
  equipments$ = this.equipmentService.equipments$;

  constructor(){
    this.sessionForm = this.formBuilder.group({
            name:['',Validators.required],
            dateTimeStart:['',Validators.required],
            dateTimeEnd:['',Validators.required],
            classroom:['',Validators.required],
            course:['',Validators.required],
            studentGroups: this.formBuilder.array([], Validators.required),
            equipments: this.formBuilder.array([], null), 
          });
  }

  ngOnInit(): void {
    const universityId = JSON.parse(sessionStorage.getItem('university')!);
    this.classRoomService.getClassrooms({universityId: universityId,siteId: undefined}).subscribe();
    this.courseService.getCourses();
    this.studentGroupService.getStudentGroups();
    this.equipmentService.getEquipments().subscribe();
    this.route.paramMap.subscribe(params => {
      this.sessionService.getSessionById(params.get('id')!).subscribe({
        next: (e) => {
          this.session = e;
          this.sessionForm.patchValue(
            {
              name: e.name,
              dateTimeStart: e.dateTimeStart,
              dateTimeEnd: e.dateTimeEnd,
              classroom: e.classroom.id,
              course:e.course.id
            }
          );
          e.studentGroups?.forEach((studentGroup) => this.studentGroupArray.push(this.formBuilder.control(studentGroup.id)));
          e.equipments?.forEach((equipment) => this.equipmentArray.push(this.formBuilder.control(equipment.id)));        
        },
        error: (e) => alert("Problem while editing the course")
      })
    })
  }

  get studentGroupArray(){
    return this.sessionForm.get('studentGroups') as FormArray;
  }

  get selectedStudentGroups(){
    return this.studentGroupArray.value;
  }

  get equipmentArray() {
        return this.sessionForm.get('equipments') as FormArray;
      }
  
    get selectedEquipments(): string[]{
      return this.equipmentArray.value;
    }

  selectEquipment(equipmentType: Equipment, event: any){
        const checked = event.target.checked;
        const indexEquipmentType = this.selectedEquipments.indexOf(equipmentType.id!);
        if (checked) {   
          this.equipmentArray.push(this.formBuilder.control(equipmentType.id));
        } else {
          this.equipmentArray.removeAt(indexEquipmentType);
        }
        this.sessionForm.controls['equipments'].markAsTouched();
  }

  selectStudentGroup(studentGroup: StudentGroup, event: any){
    const checked = event.target.checked;
    const indexStudentGroup = this.selectedStudentGroups.indexOf(studentGroup.id!);
    if (checked) {   
      this.studentGroupArray.push(this.formBuilder.control(studentGroup.id));
    } else {
      this.studentGroupArray.removeAt(indexStudentGroup);
    }
    this.sessionForm.controls['studentGroups'].markAsTouched();
  }

  onSave(){
    const session: Session = {
      ...this.sessionForm.value,
      id: this.session?.id
    }
    console.log(session);
    this.sessionService.updateSessions(session).subscribe({
      next: (v) => console.log(v),
      error: (e) => alert(e.error.detail),
      complete: () => this.router.navigate(['/schedules'])
    })
  }

  onCancel(){
    this.router.navigate(['/schedules']);
  }

}
