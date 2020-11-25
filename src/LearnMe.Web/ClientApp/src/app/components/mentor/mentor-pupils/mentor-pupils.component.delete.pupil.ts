import { Component, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../../../services/http.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";


@Component({
  selector: 'app-mentor-pupils-delete',
  templateUrl: 'mentor-pupils.component.delete.pupil.html',
  styleUrls: ['./mentor-pupils.component.css']
})

export class DeletePupilDialog  {

  description: string;

  constructor(private https: HttpService, 
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<DeletePupilDialog>,
    @Inject(MAT_DIALOG_DATA) data) {   
    this.description = data.title;   
  }

  public delete = () => {
    const route: string = 'api/UserBasics?UserEmail='+this.description;
    this.https.delete (route)
      .subscribe((result) => {
        
      },
        (error) => {
         
        });
  }
}