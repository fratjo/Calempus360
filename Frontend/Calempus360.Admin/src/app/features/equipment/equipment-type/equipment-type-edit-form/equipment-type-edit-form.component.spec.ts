import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentTypeEditFormComponent } from './equipment-type-edit-form.component';

describe('EquipmentTypeEditFormComponent', () => {
  let component: EquipmentTypeEditFormComponent;
  let fixture: ComponentFixture<EquipmentTypeEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentTypeEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentTypeEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
