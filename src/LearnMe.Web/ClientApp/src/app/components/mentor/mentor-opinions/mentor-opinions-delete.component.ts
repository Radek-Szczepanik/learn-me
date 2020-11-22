import { Component, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../../../services/http.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";


@Component({
  selector: 'app-opinions-pupils-delete',
  templateUrl: 'mentor-opinions-delete.component.html',
  styleUrls: ['./mentor-opinions.component.css']
})

export class DeleteOpinionsDialog  {

  description: string;

  constructor(private https: HttpService, 
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<DeleteOpinionsDialog>,
    @Inject(MAT_DIALOG_DATA) data) {   
    this.description = data.title;   
  }

  public delete = () => {
    const route: string = 'api/Opinions/'+this.description;
    this.https.delete (route)
      .subscribe((result) => {
        
      },
        (error) => {
         
        });
  }
}