import { Component, Inject, OnInit } from '@angular/core';
import { Login } from '../../Models/Account/login';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../services/http.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent {

  isExpanded = false;
  loginForm: FormGroup;
  loginUser: Login;
  private _httpClient: HttpClient;
  private _base: string;
  notLogged; admin; mentor; student: boolean;
  identity: string[];


  constructor(private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this._httpClient = http;
    this._base = baseUrl

    this.loginUser = {
      email: '',
      password: '',
      rememberMe: true
    };
  }

  public logIn = () => {
    const route: string = 'api/Login';
    this.https.getLogin(route, this.loginUser)
      .subscribe((result) => {
        window.location.reload()
      },
        (error) => {
          console.error(error);
        });
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
    //let temp: string[];
    //this._httpClient.get<string[]>(this._base + 'api/account').subscribe(result => {
    //  this.identity = result as string[];
    //  this.identityInfo(this.identity);
    //});
    this.initializeForm();
  }
  private initializeForm() {
    this.loginForm = new FormGroup({
      'email': new FormControl(null),
      'password': new FormControl(null),
    });
  }

  onSubmit() {
    this.loginUser = this.loginForm.value;
    this.logIn();
  }

  logOut() {
    this._httpClient.options<boolean>(this._base + 'api/account').subscribe((result) => {
      window.location.reload();
    },
      (error) => {
        console.error(error);
      });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

