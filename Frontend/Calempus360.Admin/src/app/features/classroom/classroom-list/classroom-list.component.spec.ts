import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomListComponent } from './classroom-list.component';

describe('ClassroomListComponent', () => {
  let component: ClassroomListComponent;
  let fixture: ComponentFixture<ClassroomListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassroomListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassroomListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
