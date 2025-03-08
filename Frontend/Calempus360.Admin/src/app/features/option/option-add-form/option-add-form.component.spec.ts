import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OptionAddFormComponent } from './option-add-form.component';

describe('OptionAddFormComponent', () => {
  let component: OptionAddFormComponent;
  let fixture: ComponentFixture<OptionAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OptionAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OptionAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
