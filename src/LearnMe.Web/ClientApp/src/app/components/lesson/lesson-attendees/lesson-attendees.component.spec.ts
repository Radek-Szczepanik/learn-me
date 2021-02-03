import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonAttendeesComponent } from './lesson-attendees.component';

describe('LessonAttendeesComponent', () => {
  let component: LessonAttendeesComponent;
  let fixture: ComponentFixture<LessonAttendeesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonAttendeesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonAttendeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
