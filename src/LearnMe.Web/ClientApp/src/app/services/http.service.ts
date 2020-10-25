import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Login } from '../models/Account/login';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpService: HttpClient) { }

  public getData(route: string)
  {
    return this.httpService.get(route);
  }

  public post(route: string, body: any)
  {
    return this.httpService.post(route, body);
  }

  public getIdentity(route: string)
  {
    return this.httpService.get<any>(route);
  }

  public delete(route: string) {
    return this.httpService.delete(route);
  }

  public put(route: string, body: any) {
    return this.httpService.put(route, body);
  }
}
