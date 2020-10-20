import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorCalendarComponent } from './mentor-calendar.component';

describe('MentorCalendarComponent', () => {
  let component: MentorCalendarComponent;
  let fixture: ComponentFixture<MentorCalendarComponent>;

  beforeEach(async(() => {
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
