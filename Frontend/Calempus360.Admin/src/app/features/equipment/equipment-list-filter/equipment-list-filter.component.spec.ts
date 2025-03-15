import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentListFilterComponent } from './equipment-list-filter.component';

describe('EquipmentListFilterComponent', () => {
  let component: EquipmentListFilterComponent;
  let fixture: ComponentFixture<EquipmentListFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentListFilterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentListFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
