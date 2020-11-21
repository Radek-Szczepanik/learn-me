import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PrivateLessonsComponent } from './private-lessons.component';

describe('PrivateLessonsComponent', () => {
  let component: PrivateLessonsComponent;
  let fixture: ComponentFixture<PrivateLessonsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PrivateLessonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivateLessonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
