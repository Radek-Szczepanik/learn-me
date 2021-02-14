import { HttpClient } from '@angular/common/http';
import { Component, AfterViewInit, ViewChild, Inject } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CrudService } from '../../../services/crud.service'
import { Appointment, LessonAppointmentTableEntry, Tile } from '../../../services/calendar/calendar-service-ver-2';
import {animate, state, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'app-student-lesson',
  templateUrl: './student-lesson.component.html',
  styleUrls: ['./student-lesson.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ])
  ],
})
export class StudentLessonComponent implements AfterViewInit {

  displayedColumns: string[] = ['startDate', 'startDateTime', 'endDateTime', 'subject', 'attendeesNameAndSurnameList', 'isDone'];
  expandedElement: LessonAppointmentTableEntry | null;

  dataSource: MatTableDataSource<LessonAppointmentTableEntry>;
  _http: HttpClient;
  _baseUrl: string;
  _crud: CrudService;

  // TODO: update dates by months/year
  from: Date = new Date(new Date().getTime() - (31 * 24 * 60 * 60 * 1000));
  to: Date = new Date(new Date().getTime() + (31 * 24 * 60 * 60 * 1000));

  tiles: Tile[] = [
    {text: 'One', cols: 2, rows: 1, color: 'lightblue'},
    {text: 'Two', cols: 2, rows: 1, color: 'lightgreen'},
    {text: 'Three', cols: 1, rows: 1, color: 'lightpink'},
    {text: 'Four', cols: 1, rows: 1, color: '#DDBDF1'},
  ];


  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string, 
    public dialog: MatDialog, 
    crud: CrudService) {
    this._http = http;
    this._baseUrl = baseUrl;
    this._crud = crud;
  }

  ngAfterViewInit() {
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
          }

          item.attendees.forEach(person =>{
            newItem.attendeesNameAndSurnameList.push(' ' + person.firstName + ' ' + person.lastName)
          })

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
