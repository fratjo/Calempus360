import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectUniversityDialogComponent } from './select-university-dialog.component';

describe('SelectUniversityDialogComponent', () => {
  let component: SelectUniversityDialogComponent;
  let fixture: ComponentFixture<SelectUniversityDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectUniversityDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectUniversityDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
