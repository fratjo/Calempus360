import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomEditFormComponent } from './classroom-edit-form.component';

describe('ClassroomEditFormComponent', () => {
  let component: ClassroomEditFormComponent;
  let fixture: ComponentFixture<ClassroomEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassroomEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassroomEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
