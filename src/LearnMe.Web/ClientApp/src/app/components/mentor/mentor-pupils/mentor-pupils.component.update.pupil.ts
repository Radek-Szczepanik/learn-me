import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import { Students } from '../../../models/Users/students';


@Component({
  selector: 'app-mentor-pupils-update',
  templateUrl: 'mentor-pupils.component.update.pupil.html',
  styleUrls: ['./mentor-pupils.component.css']
})

export class UpdatePupilDialog implements OnInit {

  pupilForm: FormGroup;
  pupilData: Students;
  succes: any;
  description: any;
  pupil: Students;


  constructor(private https: HttpService,
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<UpdatePupilDialog>,
    @Inject(MAT_DIALOG_DATA) data) {   

    this.description = data.title; 
    this.pupilData = {
      firstName: '',
      lastName: '',
      streetName: '',
      houseNumber: '',
      apartmentNumber: '',
      email: '',
      street: '',
      city: '',
      country: '',
      postcode: 0,
     
    };
  }

  ngOnInit() {

    this.initializeForm();
  }

  private initializeForm() {
    this.pupil = this.description;
    this.pupilForm = new FormGroup({
      'firstName': new FormControl(this.pupil.firstName),
      'lastName': new FormControl(this.pupil.lastName),
      'email': new FormControl(this.pupil.email),
      'streetName': new FormControl(this.pupil.streetName),
      'houseNumber': new FormControl(this.pupil.houseNumber),
      'apartmentNumber': new FormControl(this.pupil.apartmentNumber),
      'street': new FormControl(this.pupil.street),
      'city': new FormControl(this.pupil.city),
      'country': new FormControl(this.pupil.country),
      'postcode': new FormControl(this.pupil.postcode),
     
    });
  }

  public update = () => {
    const route: string = 'api/UserBasics';
    this.https.put(route, this.pupilData)
      .subscribe((result) => {
        this.succes = true;
      },
        (error) => {
        });
  }

  onSubmit() {
    this.succes = undefined;
    this.pupilData = this.pupilForm.value;
    this.update();
  }
}