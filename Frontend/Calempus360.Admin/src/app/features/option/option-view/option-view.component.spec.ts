import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OptionViewComponent } from './option-view.component';

describe('OptionViewComponent', () => {
  let component: OptionViewComponent;
  let fixture: ComponentFixture<OptionViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OptionViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OptionViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
