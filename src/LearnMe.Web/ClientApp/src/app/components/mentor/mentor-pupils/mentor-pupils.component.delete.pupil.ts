import { Component, Inject, OnInit } from '@angular/core';
import { Register } from '../../../models/Account/register';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";


@Component({
  selector: 'app-mentor-pupils-delete',
  templateUrl: 'mentor-pupils.component.delete.pupil.html',
  styleUrls: ['./mentor-pupils.component.css']
})

export class DeletePupilDialog implements OnInit {

  pupilForm: FormGroup;
  pupilData: Register;
  succes: any;
  private _httpClient: HttpClient;
  private _base: string;
  private errorFirstName: any;
  private errorLastName: any;
  private errorEmail: any;
  private errorPassword: any;
  description: string;


  constructor(private https: HttpService, 
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<DeletePupilDialog>,
    @Inject(MAT_DIALOG_DATA) data) {
    
    this.description = data.title;
    this._httpClient = http;
    this._base = baseUrl
    
  }

  ngOnInit() {

  }


  public delete = () => {
    const route: string = 'api/UserBasics?UserEmail='+this.description;
    this.https.delete (route)
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
    
  }
}