import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAddFormComponent } from './site-add-form.component';

describe('SiteAddFormComponent', () => {
  let component: SiteAddFormComponent;
  let fixture: ComponentFixture<SiteAddFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SiteAddFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SiteAddFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
