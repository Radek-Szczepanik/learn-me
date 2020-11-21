/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, ComponentFixture, ComponentFixtureAutoDetect, waitForAsync } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { LoginComponent } from './login.component';

let component: LoginComponent;
let fixture: ComponentFixture<LoginComponent>;

describe('login component', () => {
    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            declarations: [ LoginComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', waitForAsync(() => {
        expect(true).toEqual(true);
    }));
});