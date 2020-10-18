import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentLessonComponent } from './student-lesson.component';

describe('StudentLessonComponent', () => {
  let component: StudentLessonComponent;
  let fixture: ComponentFixture<StudentLessonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentLessonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentLessonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
