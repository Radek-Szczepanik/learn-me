import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MentorCalendarComponent } from './mentor-calendar.component';

describe('MentorCalendarComponent', () => {
  let component: MentorCalendarComponent;
  let fixture: ComponentFixture<MentorCalendarComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
