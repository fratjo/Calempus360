import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentGroupAddFormComponent } from './student-group-add-form.component';

describe('StudentGroupAddFormComponent', () => {
  let component: StudentGroupAddFormComponent;
  let fixture: ComponentFixture<StudentGroupAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentGroupAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentGroupAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
