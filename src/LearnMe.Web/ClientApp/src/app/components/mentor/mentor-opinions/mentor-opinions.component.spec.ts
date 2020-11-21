import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MentorOpinionsComponent } from './mentor-opinions.component';

describe('MentorOpinionsComponent', () => {
  let component: MentorOpinionsComponent;
  let fixture: ComponentFixture<MentorOpinionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorOpinionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorOpinionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
