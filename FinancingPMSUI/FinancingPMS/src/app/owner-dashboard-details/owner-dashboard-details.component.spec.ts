import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerDashboardDetailsComponent } from './owner-dashboard-details.component';

describe('OwnerDashboardDetailsComponent', () => {
  let component: OwnerDashboardDetailsComponent;
  let fixture: ComponentFixture<OwnerDashboardDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OwnerDashboardDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OwnerDashboardDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
