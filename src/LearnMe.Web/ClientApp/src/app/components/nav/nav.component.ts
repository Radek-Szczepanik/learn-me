import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../services/http.service';
import { Router } from '@angular/router';



@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {

  private _httpClient: HttpClient;
  private _base: string;
  notLogged; admin; mentor; student: boolean;
  identity: string[];

  constructor(private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = http;
    this._base = baseUrl;}

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
    this._httpClient.get<string[]>(this._base + 'api/account').subscribe(result => {
      this.identity = result as string[];
      this.identityInfo(this.identity);
    });
  }

  logOut() {
    this._httpClient.options<boolean>(this._base + 'api/account').subscribe((result) => {
      window.location.reload();
    },
      (error) => {
        console.error(error);
      });
  }
}
