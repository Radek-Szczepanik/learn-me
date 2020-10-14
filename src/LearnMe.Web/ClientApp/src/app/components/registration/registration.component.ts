import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';



@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})

export class RegistrationComponent implements OnInit {

  model: any = {};

  constructor() {}

  ngOnInit() {

  }

  register() {
    console.log(this.model);
  }

  cancel() {
    console.log('Akcja anulowana');
  }
}
