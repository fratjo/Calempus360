import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityAddFormComponent } from './university-add-form.component';

describe('UniversityAddFormComponent', () => {
  let component: UniversityAddFormComponent;
  let fixture: ComponentFixture<UniversityAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UniversityAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UniversityAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
