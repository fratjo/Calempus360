import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentTypeListPageComponent } from './equipment-type-list-page.component';

describe('EquipmentTypeListPageComponent', () => {
  let component: EquipmentTypeListPageComponent;
  let fixture: ComponentFixture<EquipmentTypeListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EquipmentTypeListPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EquipmentTypeListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
