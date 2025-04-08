import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomListFilterComponent } from './classroom-list-filter.component';

describe('ClassroomListFilterComponent', () => {
  let component: ClassroomListFilterComponent;
  let fixture: ComponentFixture<ClassroomListFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassroomListFilterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassroomListFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
