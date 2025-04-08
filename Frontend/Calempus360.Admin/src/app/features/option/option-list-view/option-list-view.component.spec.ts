import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OptionListViewComponent } from './option-list-view.component';

describe('OptionListViewComponent', () => {
  let component: OptionListViewComponent;
  let fixture: ComponentFixture<OptionListViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OptionListViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OptionListViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
