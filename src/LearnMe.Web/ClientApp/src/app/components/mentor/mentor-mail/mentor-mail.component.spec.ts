import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MentorMailComponent } from './mentor-mail.component';

describe('MentorMailComponent', () => {
  let component: MentorMailComponent;
  let fixture: ComponentFixture<MentorMailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorMailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorMailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
