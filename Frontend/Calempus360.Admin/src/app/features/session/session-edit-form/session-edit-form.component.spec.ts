import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionEditFormComponent } from './session-edit-form.component';

describe('SessionEditFormComponent', () => {
  let component: SessionEditFormComponent;
  let fixture: ComponentFixture<SessionEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SessionEditFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SessionEditFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
