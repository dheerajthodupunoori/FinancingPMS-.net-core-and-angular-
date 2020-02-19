import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FirmAdditionalDetailsComponent } from './firm-additional-details.component';

describe('FirmAdditionalDetailsComponent', () => {
  let component: FirmAdditionalDetailsComponent;
  let fixture: ComponentFixture<FirmAdditionalDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FirmAdditionalDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FirmAdditionalDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
