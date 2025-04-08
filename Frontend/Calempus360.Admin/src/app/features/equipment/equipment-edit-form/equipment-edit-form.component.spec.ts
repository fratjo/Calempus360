import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentEditFormComponent } from './equipment-edit-form.component';

describe('EquipmentEditFormComponent', () => {
  let component: EquipmentEditFormComponent;
  let fixture: ComponentFixture<EquipmentEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
