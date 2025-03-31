import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionAddFormComponent } from './session-add-form.component';

describe('SessionAddFormComponent', () => {
  let component: SessionAddFormComponent;
  let fixture: ComponentFixture<SessionAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SessionAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SessionAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
