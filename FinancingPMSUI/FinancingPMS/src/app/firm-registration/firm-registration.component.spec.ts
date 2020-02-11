import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FirmRegistrationComponent } from './firm-registration.component';

describe('FirmRegistrationComponent', () => {
  let component: FirmRegistrationComponent;
  let fixture: ComponentFixture<FirmRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FirmRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FirmRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
