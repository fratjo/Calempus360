import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentGroupViewComponent } from './student-group-view.component';

describe('StudentGroupViewComponent', () => {
  let component: StudentGroupViewComponent;
  let fixture: ComponentFixture<StudentGroupViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentGroupViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentGroupViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
