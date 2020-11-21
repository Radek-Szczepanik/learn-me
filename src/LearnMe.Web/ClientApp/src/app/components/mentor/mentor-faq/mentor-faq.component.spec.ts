import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MentorFaqComponent } from './mentor-faq.component';

describe('MentorFaqComponent', () => {
  let component: MentorFaqComponent;
  let fixture: ComponentFixture<MentorFaqComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorFaqComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorFaqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
