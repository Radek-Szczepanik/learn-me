import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { LessonAppointmentTableEntry } from '../../../services/calendar/calendar-service-ver-2';
import { HttpService } from '../../../services/http.service';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

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
