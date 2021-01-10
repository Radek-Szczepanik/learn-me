import { Messages } from './../models/Messages/messages';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginationResult } from '../models/Pagination/Pagination';
import { map } from 'rxjs/operators';
import { User } from '../models/Users/user';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient) { }

  getMessages(email, url): Observable<Messages[]> {
    return this.http.get<Messages[]>(url + 'api/UserBasics/' + email + 'messages')
  }

}
