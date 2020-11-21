import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StudentPaymentComponent } from './student-payment.component';

describe('StudentPaymentComponent', () => {
  let component: StudentPaymentComponent;
  let fixture: ComponentFixture<StudentPaymentComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentPaymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
