import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassroomListPageComponent } from './classroom-list-page.component';

describe('ClassroomListPageComponent', () => {
  let component: ClassroomListPageComponent;
  let fixture: ComponentFixture<ClassroomListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClassroomListPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClassroomListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
