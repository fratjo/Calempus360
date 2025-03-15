import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentTypeListComponent } from './equipment-type-list.component';

describe('EquipmentTypeListComponent', () => {
  let component: EquipmentTypeListComponent;
  let fixture: ComponentFixture<EquipmentTypeListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentTypeListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentTypeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
