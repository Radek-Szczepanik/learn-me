import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonDetailsComponent } from './lesson-details.component';

describe('LessonDetailsComponent', () => {
  let component: LessonDetailsComponent;
  let fixture: ComponentFixture<LessonDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
