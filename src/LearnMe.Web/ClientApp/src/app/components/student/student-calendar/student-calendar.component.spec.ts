import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentCalendarComponent } from './student-calendar.component';

describe('StudentCalendarComponent', () => {
  let component: StudentCalendarComponent;
  let fixture: ComponentFixture<StudentCalendarComponent>;

  beforeEach(async(() => {
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
