import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcademicYearSwitchComponent } from './academic-year-switch.component';

describe('AcademicYearSwitchComponent', () => {
  let component: AcademicYearSwitchComponent;
  let fixture: ComponentFixture<AcademicYearSwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AcademicYearSwitchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcademicYearSwitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
