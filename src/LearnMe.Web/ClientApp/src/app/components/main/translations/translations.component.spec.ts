import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TranslationsComponent } from './translations.component';

describe('TranslationsComponent', () => {
  let component: TranslationsComponent;
  let fixture: ComponentFixture<TranslationsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TranslationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TranslationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
