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
  private errorFirstName: any;
  private errorLastName: any;
  private errorPassword: any;
  private errorConfirmation: any;
  private errorEmail: any;
  private errorPasswordFront: any;

  constructor(private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = http;
    this._base = baseUrl

    this.registerUser = {
      firstname: '',
      lastname: '',
      email: '',
      password: '',
      confirmpassword: ''
    };
  }

  public register = () => {
    const route: string = 'api/Register';
    this.https.post(route, this.registerUser)
      .subscribe((result) => {
        window.location.replace("");
      },
        (error) => {
          if (error.status == "400") {

            this.errorPasswordFront = error.error.errors.Password;
            this.errorEmail = error.error.errors.Email;
            this.errorConfirmation = error.error.errors.ConfirmPassword;
            this.errorLastName = error.error.errors.FirstName;
            this.errorFirstName = error.error.errors.LastName;
          }
          else if (error.status == "401") {
            this.errorPassword = error.error.password.errors;
          }
        });
  }

  ngOnInit() {

    this.initializeForm();
  }

  private initializeForm() {
    this.registerForm = new FormGroup({
      'firstName': new FormControl(null),
      'lastName': new FormControl(null),
      'email': new FormControl(null),
      'password': new FormControl(null),
      'confirmPassword': new FormControl(null),
    });
  }

  onSubmit() {
    this.errorPassword = undefined;
    this.errorPasswordFront = undefined;
    this.errorEmail = undefined;
    this.errorConfirmation = undefined;
    this.errorFirstName = undefined;
    this.errorLastName = undefined;
    this.registerUser = this.registerForm.value;
    this.register();
  }
}
