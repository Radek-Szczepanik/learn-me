import { HttpClient } from '@angular/common/http';
import { Component, AfterViewInit, ViewChild, Inject } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CrudService } from '../../../services/crud.service'
import { Appointment, MentorLessonAppointment } from '../../../services/calendar/calendar-service-ver-2';
import {animate, state, style, transition, trigger} from '@angular/animations';

@Component({
  selector: 'app-mentor-lesson',
  templateUrl: './mentor-lesson.component.html',
  styleUrls: ['./mentor-lesson.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ])
  ],
})
export class MentorLessonComponent implements AfterViewInit {

  displayedColumns: string[] = ['startDate', 'startDateTime', 'endDateTime', 'subject', 'attendeesNameAndSurnameList'];
  expandedElement: MentorLessonAppointment | null;

  dataSource: MatTableDataSource<MentorLessonAppointment>;
  _http: HttpClient;
  _baseUrl: string;
  _crud: CrudService;

  // TODO: update dates by months/year
  from: Date = new Date(new Date().getTime() - (31 * 24 * 60 * 60 * 1000));
  to: Date = new Date(new Date().getTime() + (31 * 24 * 60 * 60 * 1000));


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
      
        let mentorLessonAppointments: MentorLessonAppointment[] = [];

        result.forEach(item =>{
          let newItem: MentorLessonAppointment = {
            calendarId: item.calendarId,
            startDate: item.startDate,
            startDateTime: item.endDate,
            endDateTime: item.endDate,
            subject: item.subject,
            attendeesNameAndSurnameList: []
          }

          item.attendees.forEach(person =>{
            newItem.attendeesNameAndSurnameList.push(person.firstName + ' ' + person.lastName)
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

  // showLesson(lesson: MentorLessonAppointment) {
  //   const dialogRef = this.dialog.open(ShowLessonDialog);

  //   dialogRef.afterClosed().subscribe(result => {
  //     this.ngAfterViewInit();
  //   });
  // }
}



  // addPupil() {
  //   const dialogRef = this.dialog.open(AddPupilDialog);

  //   dialogRef.afterClosed().subscribe(result => {
  //     this.ngAfterViewInit();
  //   });
  // }
  // deletePupil(email: string) {

  //   const dialogConfig = new MatDialogConfig();

  //   dialogConfig.data = {
  //     id: 1,
  //     title: email
  //   };

  //   const dialogRef = this.dialog.open(DeletePupilDialog, dialogConfig);

  //   dialogRef.afterClosed().subscribe(result => {
  //     this.ngAfterViewInit();
  //   });
  // }

  // updatePupil(user: Students) {

  //   const dialogConfig = new MatDialogConfig();


  //   dialogConfig.data = {
  //     id: 1,
  //     title: user
  //   };

  //   const dialogRef = this.dialog.open(UpdatePupilDialog, dialogConfig);

  //   dialogRef.afterClosed().subscribe(result => {
  //       this.getData();

  //   });
  // }
//}