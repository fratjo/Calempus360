import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentGroupEditFormComponent } from './student-group-edit-form.component';

describe('StudentGroupEditFormComponent', () => {
  let component: StudentGroupEditFormComponent;
  let fixture: ComponentFixture<StudentGroupEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentGroupEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentGroupEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
