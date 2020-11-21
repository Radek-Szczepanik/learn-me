import { Component, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../../../services/http.service';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";


@Component({
  selector: 'app-news-pupils-delete',
  templateUrl: 'mentor-news-delete.components.html',
  styleUrls: ['./mentor-news.component.css']
})

export class DeleteNewsDialog  {

  description: string;

  constructor(private https: HttpService, 
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<DeleteNewsDialog>,
    @Inject(MAT_DIALOG_DATA) data) {   
    this.description = data.title;   
  }

  public delete = () => {
    const route: string = 'api/News/'+this.description;
    this.https.delete (route)
      .subscribe((result) => {
        
      },
        (error) => {
         
        });
  }
}