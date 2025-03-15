import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentAddFormComponent } from './equipment-add-form.component';

describe('EquipmentAddFormComponent', () => {
  let component: EquipmentAddFormComponent;
  let fixture: ComponentFixture<EquipmentAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
