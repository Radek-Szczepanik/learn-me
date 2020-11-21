import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MentorLessonComponent } from './mentor-lesson.component';

describe('MentorLessonComponent', () => {
  let component: MentorLessonComponent;
  let fixture: ComponentFixture<MentorLessonComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorLessonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorLessonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
