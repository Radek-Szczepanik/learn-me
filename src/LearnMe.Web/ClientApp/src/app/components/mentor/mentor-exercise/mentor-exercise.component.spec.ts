import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MentorExerciseComponent } from './mentor-exercise.component';

describe('MentorExerciseComponent', () => {
  let component: MentorExerciseComponent;
  let fixture: ComponentFixture<MentorExerciseComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorExerciseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorExerciseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
