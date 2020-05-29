import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerAdditionalDetailsComponent } from './customer-additional-details.component';

describe('CustomerAdditionalDetailsComponent', () => {
  let component: CustomerAdditionalDetailsComponent;
  let fixture: ComponentFixture<CustomerAdditionalDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerAdditionalDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerAdditionalDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
