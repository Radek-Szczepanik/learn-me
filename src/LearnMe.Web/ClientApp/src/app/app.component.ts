import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, HostListener, OnDestroy, ViewChild } from '@angular/core';
import { Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../app/services/http.service';
import { MatSidenav } from '@angular/material/sidenav';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
 })

export class AppComponent implements OnInit {
  title = 'app';
  mobileQuery: MediaQueryList;
  private _httpClient: HttpClient;
  private _base: string;
  notLogged; admin; mentor; student: boolean;
  private identity: string[];
  private _mobileQueryListener: () => void;
  public isMobile: boolean;
  @ViewChild('sidenav') sidenav: MatSidenav;


  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
    this._httpClient = http;
    this._base = baseUrl;
  }
  
  // ngOnDestroy(): void {
  //   this.mobileQuery.removeListener(this._mobileQueryListener);
  // }

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
    this._httpClient.get<string[]>(this._base + 'api/Identity').subscribe(result => {
      this.identity = result as string[];
      this.identityInfo(this.identity);
    });

    this.isMobile = window.innerWidth < 1330 ? true : false; 
  }

  logOut() {
    this._httpClient.get(this._base + 'api/Login').subscribe((result) => {
      window.location.replace(".")
    },
      (error) => {
        console.error(error);
      });
  }

  @HostListener('window:resize')
  onResize() {
    this.isMobile = window.innerWidth < 1330 ? true : false; 
  };
}