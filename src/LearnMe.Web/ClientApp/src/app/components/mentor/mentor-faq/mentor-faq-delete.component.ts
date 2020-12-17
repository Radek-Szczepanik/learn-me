import { Component, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../../../services/http.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";


@Component({
  selector: 'app-faq-pupils-delete',
  templateUrl: './mentor-faq-delete.component.html',
  styleUrls: ['./mentor-faq.component.css']
})

export class DeleteFaqDialog  {

  description: string;

  constructor(private https: HttpService, 
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<DeleteFaqDialog>,
    @Inject(MAT_DIALOG_DATA) data) {   
    this.description = data.title;   
  }

  public delete = () => {
    const route: string = 'api/Questions/'+this.description;
    this.https.delete (route)
      .subscribe((result) => {
        
      },
        (error) => {
         
        });
  }
}