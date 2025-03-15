import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteListPageComponent } from './site-list-page.component';

describe('SiteListPageComponent', () => {
  let component: SiteListPageComponent;
  let fixture: ComponentFixture<SiteListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SiteListPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SiteListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
