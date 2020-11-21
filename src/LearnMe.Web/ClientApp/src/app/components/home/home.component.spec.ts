/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, ComponentFixture, ComponentFixtureAutoDetect, waitForAsync } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { HomeComponent } from './home.component';

let component: HomeComponent;
let fixture: ComponentFixture<HomeComponent>;

describe('home component', () => {
    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            declarations: [ HomeComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', waitForAsync(() => {
        expect(true).toEqual(true);
    }));
});