import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StudentCalendarComponent } from './student-calendar.component';

describe('StudentCalendarComponent', () => {
  let component: StudentCalendarComponent;
  let fixture: ComponentFixture<StudentCalendarComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
