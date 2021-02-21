import { Messages } from './../../../models/Messages/messages';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { HttpClient } from '@angular/common/http';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { AddStudentMailDialog } from './mentor-mail-add.component';
import { DeleteStudentMailDialog } from './mentor-mail-delete.component'
import { Students } from 'src/app/models/Users/students';


@Component({
  selector: 'app-student-mail',
  templateUrl: './student-mail.component.html',
  styleUrls: ['./student-mail.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ])
  ],
})

export class StudentMailComponent implements OnInit {

  displayedColumns: string[] = ['imgPath', 'date', 'title', 'name', 'email', 'actions'];

  dataSource: MatTableDataSource<Messages>;
  _http: HttpClient;
  _baseUrl: string;
  mailList: Students[];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  messages: Messages[];
  private _httpClient: HttpClient;
  private _base: string;
  private userIdentity: string[];
  private messagesList: Messages[];
  boxes: string = 'Inbox';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  getMessages() {
    this._http.get<Messages[]>(this._baseUrl + 'api/Messages?email=' + this.userIdentity[1] + '&MessageContainer=' + this.boxes).subscribe(result => {
      this.messagesList = result;
      this.getIdentityForUser();
      this.dataSource = new MatTableDataSource(result);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  getIdentityForUser() {
    this._http.get<Students[]>(this._baseUrl + 'api/UserBasics?rolename=mentor').subscribe(result => {
      this.mailList = result;
      this.findImgPatch();
    });
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getIdentity() {
    this._http.get<string[]>(this._baseUrl + 'api/Identity').subscribe(result => {
      this.userIdentity = result as string[];
      this.getMessages()
    })
  }

  addMail() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '30%';
    // dialogConfig.height= '50%';
    dialogConfig.data = {
      userIdentity: this.userIdentity,
      mailList: this.mailList
    }
    const dialogRef = this.dialog.open( AddStudentMailDialog, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  findImgPatch() {
    this.mailList.forEach(element => {
      this.messagesList.forEach(element2 => {
        if (element2.senderEmail == element.email) {
          element2.imgPath = element.imgPath
        }
      });
    });
  }

  deleteMail(message: any) {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      mailId: message.id,
      senderFirstName: message.senderFirstName,
      senderLastName: message.senderLastName,
      title: message.title
    };


    const dialogRef = this.dialog.open(DeleteStudentMailDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  getBox(box: string) {
    this.boxes = box;
    this.ngOnInit();
  }

  ngOnInit() {
    this.getIdentity();

  }


}
