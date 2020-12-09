import { Messages } from './../models/Messages/messages';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = 'https://localhost:5001/api/messages';

  constructor(private http: HttpClient) { }

  getMessages(): Observable<Messages[]> {
    return this.http.get<Messages[]>(this.baseUrl);
  }

  getMessage(id: number): Observable<Messages> {
    return this.http.get<Messages>(this.baseUrl + '/' + id);
  }
}
