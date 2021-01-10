import { Messages } from './../../../models/Messages/messages';
import { MessageService } from './../../../services/message.service';
import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { Inject } from '@angular/core';
import { User } from 'src/app/models/Users/user';

@Component({
  selector: 'app-mentor-mail',
  templateUrl: './mentor-mail.component.html',
  styleUrls: ['./mentor-mail.component.css']
})

export class MentorMailComponent {

  displayedColumns: string[] = ['data', 'title', 'firstName', 'lastName', 'actions'];

  dataSource: MatTableDataSource<Messages>;
  _http: HttpClient;
  _baseUrl: string;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  messages: Messages[];
  users: User[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog, private messageService: MessageService) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  // ngAfterViewInit() {
  //   this._http.get<Messages[]>(this._baseUrl + 'api/Messages').subscribe(result => {
  //     this.dataSource = new MatTableDataSource(result);
  //     this.dataSource.sort = this.sort;
  //     this.dataSource.paginator = this.paginator;
  //   });
  // }

  loadMessages() {
    this.messageService.getMessages().subscribe((messages: Messages[]) => {
      this.messages = messages;
    }, error => {
      console.log(error);
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngOnInit() {
    this.loadMessages();
  }
}
