import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorLessonComponent } from './mentor-lesson.component';

describe('MentorLessonComponent', () => {
  let component: MentorLessonComponent;
  let fixture: ComponentFixture<MentorLessonComponent>;

  beforeEach(async(() => {
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
