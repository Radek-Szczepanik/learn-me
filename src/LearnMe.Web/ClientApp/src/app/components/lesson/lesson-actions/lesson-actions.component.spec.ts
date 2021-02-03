import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonActionsComponent } from './lesson-actions.component';

describe('LessonActionsComponent', () => {
  let component: LessonActionsComponent;
  let fixture: ComponentFixture<LessonActionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonActionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
