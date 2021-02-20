import { Component, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../../../services/http.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";


@Component({
  selector: 'app-mentor-mail-delete',
  templateUrl: 'mentor-mail-delete.component.html',
//   styleUrls: ['./mentor-news.component.css']
})

export class DeleteMailDialog  {

  description: any;

  constructor(private https: HttpClient, 
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<DeleteMailDialog>,
    @Inject(MAT_DIALOG_DATA) data) {   
    this.description = data;   
  }

  public delete = () => {
    const route: string = 'api/Messages?id=' + this.description.mailId;
    this.https.delete(route)
      .subscribe((result) => {
      },
        (error) => {
         
        });
  }
}