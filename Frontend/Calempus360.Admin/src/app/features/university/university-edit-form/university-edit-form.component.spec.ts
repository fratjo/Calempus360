import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityEditFormComponent } from './university-edit-form.component';

describe('UniversityEditFormComponent', () => {
  let component: UniversityEditFormComponent;
  let fixture: ComponentFixture<UniversityEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UniversityEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UniversityEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
