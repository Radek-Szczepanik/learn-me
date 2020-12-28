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

  baseUrl = 'https://localhost:5001/api/UserBasics/';

  constructor(private http: HttpClient) { }

  getMessages(): Observable<Messages[]> {
    return this.http.get<Messages[]>(this.baseUrl + '230286bd-d596-442a-a7ae-46a78b6f4dc8/' + 'messages')
  }

}
