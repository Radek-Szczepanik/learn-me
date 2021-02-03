import { Component, Input, OnInit } from '@angular/core';
import { LessonAppointmentTableEntry } from '../../../services/calendar/calendar-service-ver-2';
import { HttpService } from '../../../services/http.service';

@Component({
  selector: 'app-lesson-materials',
  templateUrl: './lesson-materials.component.html',
  styleUrls: ['./lesson-materials.component.css']
})
export class LessonMaterialsComponent implements OnInit {

  @Input()
  lesson: LessonAppointmentTableEntry;

  loggedUser: string[] = ['Student'];

  constructor(private https: HttpService) { }

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

}
