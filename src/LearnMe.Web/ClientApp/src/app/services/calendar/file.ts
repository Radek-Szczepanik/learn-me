import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpService: HttpClient) { }

  public getData(route: string)
  {
    return this.httpService.get(route);
  }
}
