import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonMaterialsComponent } from './lesson-materials.component';

describe('LessonMaterialsComponent', () => {
  let component: LessonMaterialsComponent;
  let fixture: ComponentFixture<LessonMaterialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonMaterialsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonMaterialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
