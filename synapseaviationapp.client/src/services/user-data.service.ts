import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {
 private apiUrl: string = environment.apiUrl
  constructor(private http: HttpClient) { }



  getAllUsers(): Observable<any> {
    return this.http.get(this.apiUrl + 'api/User/GetAllUsers')
  }

  updateUser(user: any): Observable<any> {
    const url = `${this.apiUrl}api/User/${user.id}`;
    return this.http.put<any>(url, user);
  }


}

