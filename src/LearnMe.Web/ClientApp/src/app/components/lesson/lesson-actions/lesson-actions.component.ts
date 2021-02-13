import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LessonAppointmentTableEntry } from '../../../services/calendar/calendar-service-ver-2';
import { HttpService } from '../../../services/http.service';

@Component({
  selector: 'app-lesson-actions',
  templateUrl: './lesson-actions.component.html',
  styleUrls: ['./lesson-actions.component.css']
})
export class LessonActionsComponent implements OnInit {

  @Input()
  lesson: LessonAppointmentTableEntry;

  loggedUser: string[] = ['Student'];
  message: string;
  fileStream: string;
  @Output() public onUploadFinished = new EventEmitter();
  progress: number;

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

  public uploadFile = (files, lessonCalendarId) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    formData.append('lessonCalendarId', lessonCalendarId);

    // let postRoute = 'api/homework'; //lessonCalendarId?lessonCalendarId=' + lessonCalendarId;
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
