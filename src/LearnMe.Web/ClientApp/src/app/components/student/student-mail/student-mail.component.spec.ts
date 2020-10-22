import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentMailComponent } from './student-mail.component';

describe('StudentMailComponent', () => {
  let component: StudentMailComponent;
  let fixture: ComponentFixture<StudentMailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentMailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentMailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
