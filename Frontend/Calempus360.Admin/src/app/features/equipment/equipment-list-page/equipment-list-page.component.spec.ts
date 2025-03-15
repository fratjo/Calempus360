import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentListPageComponent } from './equipment-list-page.component';

describe('EquipmentListPageComponent', () => {
  let component: EquipmentListPageComponent;
  let fixture: ComponentFixture<EquipmentListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentListPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
