import { Component, Inject, OnInit } from '@angular/core';
import { Register } from '../../Models/Account/register';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../services/http.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})

export class RegistrationComponent implements OnInit {

  isExpanded = false;
  registerForm: FormGroup;
  registerUser: Register;
  private _httpClient: HttpClient;
  private _base: string;
  private errorMail: string;
  private errorPassword: string;
  private errorConfirmation: string;
  private errorBackend: any;
  private badrequest: boolean;
  private unauthorized: boolean;
  notLogged; admin; mentor; student: boolean;
  identity: string[];

  constructor(private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = http;
    this._base = baseUrl

    this.registerUser = {
      email: '',
      password: '',
      confirmpassword: ''
    };
  }

  public register = () => {
    this.badrequest = false;
    const route: string = 'api/Register';
    this.https.post(route, this.registerUser)
      .subscribe((result) => {
        window.location.replace("");
      },
        (error) => {
          if (error.status == "400") {
           
            this.errorMail = error.error.errors.Email;
            this.errorPassword = error.error.errors.Password;
            this.errorConfirmation = error.error.errors.ConfirmPassword;
            this.errorBackend = error.errors;
          }
          else if (error.status == "401") {
            this.unauthorized = true;
          }
        });
  }

  ngOnInit() {

    this.initializeForm();
  }

  private initializeForm() {
    this.registerForm = new FormGroup({
      'email': new FormControl(null),
      'password': new FormControl(null),
      'confirmPassword': new FormControl(null),
    });
  }

  onSubmit() {
    this.registerUser = this.registerForm.value;
    this.register();
  }

}
