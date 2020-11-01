import { Component, Inject, OnInit } from '@angular/core';
import { Register } from '../../../models/Account/register';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';


@Component({
  selector: 'dialog-content-example-dialog',
  templateUrl: 'mentor-pupils.componebt.add.pupil.html',
  styleUrls: ['./mentor-pupils.component.css']
})

export class DialogContentExampleDialog implements OnInit {

  pupilForm: FormGroup;
  pupilData: Register;
  succes: any;
  private _httpClient: HttpClient;
  private _base: string;
  private errorFirstName: any;
  private errorLastName: any;
  private errorEmail: any;
  private errorPassword: any;


  constructor(private https: HttpService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = http;
    this._base = baseUrl
    this.pupilData = {
      firstname: '',
      lastname: '',
      streetname: '',
      housenumber: '',
      apartmentnumber: 0,
      email: '',
      password: 'temp',
      street: '',
      city: '',
      country: '',
      postcode: 0,
      confirmpassword: 'temp'
    };
  }

  ngOnInit() {

    this.initializeForm();
  }

  private initializeForm() {
    this.pupilForm = new FormGroup({
      'firstName': new FormControl(null),
      'lastName': new FormControl(null),
      'email': new FormControl(null, Validators.required),
      'password': new FormControl('Temp1!'),
      'confirmPassword': new FormControl('Temp1!'),
      'streetName': new FormControl(null),
      'houseNumber': new FormControl(null),
      'apartmentNumber': new FormControl(null),
      'street': new FormControl(null),
      'city': new FormControl(null),
      'country': new FormControl(null),
      'postcode': new FormControl(null),
     
    });
  }

  public add = () => {
    const route: string = 'api/RegisterByMentor';
    this.https.post(route, this.pupilData)
      .subscribe((result) => {
        this.succes = true;
      },
        (error) => {
          if (error.status == "400") {
            this.errorEmail = error.error.errors.Email;
            this.errorLastName = error.error.errors.FirstName;
            this.errorFirstName = error.error.errors.LastName;
          }
          else if (error.status == "401") {
            this.errorPassword = error.error.password.errors;
          }
        });
  }
  onSubmit() {
    this.errorPassword = undefined;
    this.errorEmail = undefined;
    this.errorFirstName = undefined;
    this.errorLastName = undefined;
    this.succes = undefined;
    this.pupilData = this.pupilForm.value;
    this.add();
  }
}