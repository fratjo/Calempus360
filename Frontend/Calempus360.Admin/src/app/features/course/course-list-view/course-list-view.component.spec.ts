import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseListViewComponent } from './course-list-view.component';

describe('CourseListViewComponent', () => {
  let component: CourseListViewComponent;
  let fixture: ComponentFixture<CourseListViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseListViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseListViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
