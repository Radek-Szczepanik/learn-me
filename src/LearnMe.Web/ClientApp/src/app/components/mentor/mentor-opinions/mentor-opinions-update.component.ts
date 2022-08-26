import { Component, Inject, OnInit, Output, EventEmitter  } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { News } from "../../../models/Home/news";
import { Opinion } from 'src/app/models/Home/opinon';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";


@Component({
  selector: 'app-mentor-opinions-update',
  templateUrl: 'mentor-opinions-update.component.html',
  styleUrls: ['./mentor-opinions.component.css']
})

export class UpdateOpinionsDialog implements OnInit {

    opinionForm: UntypedFormGroup;
    opinionData: Opinion;
    succes: any;
    description: any;
    opinion: Opinion;
    public progress: number;
    public message: string;
  
  
  
    constructor(private https: HttpService,
      @Inject('BASE_URL') baseUrl: string,
      private dialogRef: MatDialogRef<UpdateOpinionsDialog>,
      @Inject(MAT_DIALOG_DATA) data,
      private http: HttpClient) {   
  
      this.description = data.title; 
      this.opinionData = {
        title: '',
        text: '',
        date: '',
        author: '',
        rating: 0,
        id: 0,
      };
    }
  
    ngOnInit() {
  
      this.initializeForm();
    }
  
  
    private initializeForm() {
      this.opinion = this.description;
      this.opinionForm = new UntypedFormGroup({
          'id': new UntypedFormControl(this.opinion.id),
          'author': new UntypedFormControl(this.opinion.author),
          'title': new UntypedFormControl('temp'),
          'text': new UntypedFormControl(this.opinion.text),
          'date': new UntypedFormControl(this.opinion.date),
          'rating': new UntypedFormControl(this.opinion.rating),
      });
    }
  
    public update = () => {
      const route: string = 'api/Opinions';
      this.https.put(route, this.opinionData)
        .subscribe((result) => {
          this.succes = true;
        },
          (error) => {
          });
    }
  
    onSubmit() {
      this.succes = undefined;
      this.opinionData = this.opinionForm.value;
      this.update();
    }
  }