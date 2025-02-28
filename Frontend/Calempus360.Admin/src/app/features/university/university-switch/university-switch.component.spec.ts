import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversitySwitchComponent } from './university-switch.component';

describe('UniversitySwitchComponent', () => {
  let component: UniversitySwitchComponent;
  let fixture: ComponentFixture<UniversitySwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UniversitySwitchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UniversitySwitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
