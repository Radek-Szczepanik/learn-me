import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorPaymentComponent } from './mentor-payment.component';

describe('MentorPaymentComponent', () => {
  let component: MentorPaymentComponent;
  let fixture: ComponentFixture<MentorPaymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorPaymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
