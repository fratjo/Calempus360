import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DockBarComponent } from './dock-bar.component';

describe('DockBarComponent', () => {
  let component: DockBarComponent;
  let fixture: ComponentFixture<DockBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DockBarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DockBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
