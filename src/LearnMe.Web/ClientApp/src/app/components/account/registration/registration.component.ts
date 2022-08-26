import { Component, Inject, OnInit } from '@angular/core';
import { Register } from '../../../models/Account/register';
import { HttpClient } from '@angular/common/http';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})

export class RegistrationComponent implements OnInit {

  isExpanded = false;
  userForm: UntypedFormGroup;
  userData: Register;
  error: any;
  private _httpClient: HttpClient;
  private _base: string;
  errorFirstName: any;
  errorLastName: any;
  errorPassword: any;
  errorConfirmation: any;
  errorEmail: any;
  errorPasswordFront: any;
  unauthorized: boolean;


  constructor(private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = http;
    this._base = baseUrl

    this.userData = {
      firstName: '',
      lastName: '',
      streetName:'',
      houseNumber:'',
      apartmentNumber:'',
      email: '',
      password: '',
      street:'',
      city: '',
      country:'',
      postcode: 0,
      confirmPassword: '',
      imgPath: ''
    };
  }

  public register = () => {
    const route: string = 'api/Register';
    this.https.post(route, this.userData)
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
    this.userForm = new UntypedFormGroup({
      'firstName': new UntypedFormControl(null),
      'lastName': new UntypedFormControl(null),
      'email': new UntypedFormControl(null),
      'password': new UntypedFormControl(null),
      'confirmPassword': new UntypedFormControl(null),
    });
  }

  onSubmit() {
    this.error = undefined;
    this.errorPassword = undefined;
    this.errorPasswordFront = undefined;
    this.errorEmail = undefined;
    this.errorConfirmation = undefined;
    this.errorFirstName = undefined;
    this.errorLastName = undefined;
    this.userData = this.userForm.value;
    this.register();
  }
}
