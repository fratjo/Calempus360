import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomAddFormComponent } from './classroom-add-form.component';

describe('ClassroomAddFormComponent', () => {
  let component: ClassroomAddFormComponent;
  let fixture: ComponentFixture<ClassroomAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassroomAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassroomAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
