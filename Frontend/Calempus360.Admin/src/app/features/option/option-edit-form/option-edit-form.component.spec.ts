import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OptionEditFormComponent } from './option-edit-form.component';

describe('OptionEditFormComponent', () => {
  let component: OptionEditFormComponent;
  let fixture: ComponentFixture<OptionEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OptionEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OptionEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
