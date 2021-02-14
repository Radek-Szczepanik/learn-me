import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LessonAppointmentTableEntry } from '../../../services/calendar/calendar-service-ver-2';
import { HttpService } from '../../../services/http.service';

@Component({
  selector: 'app-lesson-attendees',
  templateUrl: './lesson-attendees.component.html',
  styleUrls: ['./lesson-attendees.component.css']
})
export class LessonAttendeesComponent implements OnInit {

  @Input()
  lesson: LessonAppointmentTableEntry;

  loggedUser: string[] = ['Student'];

  constructor(private https: HttpService, private http: HttpClient) { }

  ngOnInit(): void {
    const routeGetLoggedUser: string  = '/api/Identity';
    
    this.https.getData(routeGetLoggedUser)
    .toPromise().then(success => {
      if (success) {
        this.loggedUser = success as string[];
      }
    });

    console.debug('this.lesson.allLessonHomeworksDone');
    console.debug(this.lesson.allLessonHomeworksDone);
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
