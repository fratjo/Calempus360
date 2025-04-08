import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OptionFilterViewComponent } from './option-filter-view.component';

describe('OptionFilterViewComponent', () => {
  let component: OptionFilterViewComponent;
  let fixture: ComponentFixture<OptionFilterViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OptionFilterViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OptionFilterViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
