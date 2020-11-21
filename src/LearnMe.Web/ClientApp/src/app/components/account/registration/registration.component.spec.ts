/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, ComponentFixture, ComponentFixtureAutoDetect, waitForAsync } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { RegistrationComponent } from './registration.component';

let component: RegistrationComponent;
let fixture: ComponentFixture<RegistrationComponent>;

describe('register component', () => {
    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
          declarations: [RegistrationComponent],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
      fixture = TestBed.createComponent(RegistrationComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', waitForAsync(() => {
        expect(true).toEqual(true);
    }));
});
