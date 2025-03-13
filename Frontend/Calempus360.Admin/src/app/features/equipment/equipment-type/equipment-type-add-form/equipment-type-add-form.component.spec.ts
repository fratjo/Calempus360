import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentTypeAddFormComponent } from './equipment-type-add-form.component';

describe('EquipmentTypeAddFormComponent', () => {
  let component: EquipmentTypeAddFormComponent;
  let fixture: ComponentFixture<EquipmentTypeAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentTypeAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentTypeAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
