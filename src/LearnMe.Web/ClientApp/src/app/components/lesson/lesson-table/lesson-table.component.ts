import { HttpClient } from '@angular/common/http';
import { Component, AfterViewInit, ViewChild, Inject } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CrudService } from '../../../services/crud.service'
import { Appointment, LessonAppointmentTableEntry, Tile } from '../../../services/calendar/calendar-service-ver-2';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { HomeworkDto } from '../../../Models/Lesson/lesson';
import { HttpService } from '../../../services/http.service';

@Component({
  selector: 'app-lesson-table',
  templateUrl: './lesson-table.component.html',
  styleUrls: ['./lesson-table.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ])
  ],
})
export class LessonTableComponent implements AfterViewInit {

  displayedColumns: string[] = ['startDate', 'startDateTime', 'endDateTime', 'subject', 'attendeesNameAndSurnameList', 'isDone'];
  expandedElement: LessonAppointmentTableEntry | null;

  loggedUser: string[] = ['Student'];

  dataSource: MatTableDataSource<LessonAppointmentTableEntry>;
  _http: HttpClient;
  _baseUrl: string;
  _crud: CrudService;

  // TODO: update dates by months/year
  // from: Date = new Date(new Date().getTime() - (31 * 24 * 60 * 60 * 1000));
  // to: Date = new Date(new Date().getTime() + (31 * 24 * 60 * 60 * 1000));
  from: Date = new Date(new Date().getTime() - (90 * 24 * 60 * 60 * 1000));
  to: Date = new Date(new Date().getTime() + (90 * 24 * 60 * 60 * 1000));

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;


  constructor(http: HttpClient, private https: HttpService,
    @Inject('BASE_URL') baseUrl: string, 
    public dialog: MatDialog, 
    crud: CrudService) {
    this._http = http;
    this._baseUrl = baseUrl;
    this._crud = crud;
  }

  ngAfterViewInit() {

    const routeGetLoggedUser: string  = '/api/Identity';
    
    this.https.getData(routeGetLoggedUser)
    .toPromise().then(success => {
      if (success) {
        this.loggedUser = success as string[];
        console.debug('loggedUser');
        console.debug(this.loggedUser);
      }
    });
    
   this.getData();
  }

  getData(){
    
      this._http.get<Appointment[]>(this._baseUrl + 'api/calendareventsbydate?fromDate=' + this.from.toJSON() + '&toDate=' + this.to.toJSON()).subscribe(result => {
      
        let mentorLessonAppointments: LessonAppointmentTableEntry[] = [];

        result.forEach(item =>{
          let newItem: LessonAppointmentTableEntry = {
            subject: item.subject,
            description: item.description,
            startDate: item.startDate,
            startDateTime: item.startDate,
            endDateTime: item.endDate,
            isDone: item.isDone,
            isFreeSlot: item.isFreeSlot,
            calendarId: item.calendarId,
            lesson: item.lesson,
            attendees: item.attendees,
            attendeesNameAndSurnameList: [],
            relatedMaterials: [],
            loggedStudentHomeworks: [],
            allLessonHomeworksDone: [],
          }

          item.attendees.forEach(person =>{
            newItem.attendeesNameAndSurnameList.push(' ' + person.firstName + ' ' + person.lastName)
          })

          const routeGetLessonHomeworks: string  = '/api/HomeworkByCalendarId/' + item.calendarId;
    
          this.https.getData(routeGetLessonHomeworks)
          .toPromise().then(success => {
            if (success) {
              newItem.relatedMaterials = success as HomeworkDto[];
              console.debug('relatedMaterials');
              console.debug(newItem.relatedMaterials);
            }
          });

          const routeGetStudentLessonHomeworks: string  = '/api/HomeworkByCalendarId'
            + '?lessonCalendarId=' + item.calendarId
            + '&userEmail=' + this.loggedUser[1];
    
          this.https.getData(routeGetStudentLessonHomeworks)
          .toPromise().then(success => {
            if (success) {
              newItem.loggedStudentHomeworks = success as HomeworkDto[];
              console.debug('loggedStudentHomeworks');
              console.debug(newItem.loggedStudentHomeworks);
            }
          });

          const routeGetAllDoneLessonHomeworks: string  = '/api/HomeworkDone'
            + '?lessonCalendarId=' + item.calendarId;
    
          this.https.getData(routeGetAllDoneLessonHomeworks)
          .toPromise().then(success => {
            if (success) {
              newItem.allLessonHomeworksDone = success as HomeworkDto[];
              console.debug('allLessonHomeworksDone');
              console.debug(newItem.allLessonHomeworksDone);
            }
          });

          mentorLessonAppointments.push(newItem);
        });
      
        this.dataSource = new MatTableDataSource(mentorLessonAppointments);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}