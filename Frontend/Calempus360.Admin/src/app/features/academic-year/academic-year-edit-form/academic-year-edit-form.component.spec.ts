import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcademicYearEditFormComponent } from './academic-year-edit-form.component';

describe('AcademicYearEditFormComponent', () => {
  let component: AcademicYearEditFormComponent;
  let fixture: ComponentFixture<AcademicYearEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AcademicYearEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcademicYearEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
