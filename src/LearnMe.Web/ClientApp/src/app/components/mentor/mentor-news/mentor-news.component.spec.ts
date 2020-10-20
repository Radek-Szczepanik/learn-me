import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorNewsComponent } from './mentor-news.component';

describe('MentorNewsComponent', () => {
  let component: MentorNewsComponent;
  let fixture: ComponentFixture<MentorNewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MentorNewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorNewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
