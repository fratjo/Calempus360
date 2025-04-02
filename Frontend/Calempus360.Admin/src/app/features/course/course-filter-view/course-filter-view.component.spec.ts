import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseFilterViewComponent } from './course-filter-view.component';

describe('CourseFilterViewComponent', () => {
  let component: CourseFilterViewComponent;
  let fixture: ComponentFixture<CourseFilterViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseFilterViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseFilterViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
