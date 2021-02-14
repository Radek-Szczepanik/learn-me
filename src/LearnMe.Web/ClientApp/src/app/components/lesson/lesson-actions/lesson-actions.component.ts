import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
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
        console.debug('success');
        console.debug(success);
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

    let postRoute = 'api/homework?postingUserRole=' + this.loggedUser[0]
      + '&postingUserEmail=' + this.loggedUser[1];
    this.http.post(postRoute, formData, {reportProgress: true, observe: 'events'})
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

  public downloadFile(fileName) {
    this.downloadFileFromApi(fileName)
      .subscribe((resultBlob: Blob) => {
        var downloadURL = URL.createObjectURL(resultBlob);
        
        var link = document.createElement('a');
        link.href = downloadURL;
        link.download = fileName;
        link.click();
      });
  }

  private downloadFileFromApi(fileName): Observable<any> {
    let route = 'api/download/' + fileName;
    let url = encodeURI(route);

		return this.http.get(url, { responseType: "blob" });
   }
}
