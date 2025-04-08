import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcademicYearAddFormComponent } from './academic-year-add-form.component';

describe('AcademicYearAddFormComponent', () => {
  let component: AcademicYearAddFormComponent;
  let fixture: ComponentFixture<AcademicYearAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AcademicYearAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcademicYearAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
