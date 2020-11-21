import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MentorPupilsComponent } from './mentor-pupils.component';

describe('MentorPupilsComponent', () => {
  let component: MentorPupilsComponent;
  let fixture: ComponentFixture<MentorPupilsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorPupilsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorPupilsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
