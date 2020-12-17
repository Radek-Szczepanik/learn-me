import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnDestroy } from '@angular/core';
import { Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../app/services/http.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
 })

export class AppComponent implements OnDestroy, OnInit {
  title = 'app';
  mobileQuery: MediaQueryList;
  private _httpClient: HttpClient;
  private _base: string;
  notLogged; admin; mentor; student: boolean;
  identity: string[];
  private _mobileQueryListener: () => void;

  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
    this._httpClient = http;
    this._base = baseUrl;
  }
  
  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  public identityInfo(list: string[]) {

    if (list[0] == "Admin") {
      this.admin = true;
    }
    else if (list[0] == "Mentor") {
      this.mentor = true;
    }
    else if (list[0] == "Student") {
      this.student = true;
    }
    else if (list[0] == null) {
      this.notLogged = true;
    }
  }

  ngOnInit() {
    let temp: string[];
    this._httpClient.get<string[]>(this._base + 'api/Identity').subscribe(result => {
      this.identity = result as string[];
      this.identityInfo(this.identity);
    });
  }

  logOut() {
    this._httpClient.get(this._base + 'api/Login').subscribe((result) => {
      window.location.reload();
    },
      (error) => {
        console.error(error);
      });
  }
}