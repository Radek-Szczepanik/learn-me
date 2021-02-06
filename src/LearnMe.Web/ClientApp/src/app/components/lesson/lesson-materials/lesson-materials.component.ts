import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { LessonAppointmentTableEntry } from '../../../services/calendar/calendar-service-ver-2';
import { HttpService } from '../../../services/http.service';
import { HttpEventType, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-lesson-materials',
  templateUrl: './lesson-materials.component.html',
  styleUrls: ['./lesson-materials.component.css']
})
export class LessonMaterialsComponent implements OnInit {

  @Input()
  lesson: LessonAppointmentTableEntry;

  loggedUser: string[] = ['Student'];
  
  fileStream: string;
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private https: HttpService, private http: HttpClient) { }

  ngOnInit(): void {
    const routeGetLoggedUser: string  = '/api/Identity';
    
    this.https.getData(routeGetLoggedUser)
    .toPromise().then(success => {
      if (success) {
        this.loggedUser = success as string[];
        console.debug('loggedUser');
        console.debug(this.loggedUser);
      }
    });
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post('api/homework', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
          this.fileStream = fileToUpload.name;
        }
      });
  }

}
