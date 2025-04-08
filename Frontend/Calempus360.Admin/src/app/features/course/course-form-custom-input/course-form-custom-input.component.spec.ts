import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseFormCustomInputComponent } from './course-form-custom-input.component';

describe('CourseFormCustomInputComponent', () => {
  let component: CourseFormCustomInputComponent;
  let fixture: ComponentFixture<CourseFormCustomInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseFormCustomInputComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseFormCustomInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
