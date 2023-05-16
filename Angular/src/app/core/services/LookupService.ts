import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Gender } from '../models/Gender';
import { User } from '../models/User';
import { AuthService } from './AuthService';

@Injectable({
  providedIn: 'root'
})
export class LookUpService {

  constructor(private http: HttpClient, private authService: AuthService) {}

  getAllGenders(): Observable<Gender[]> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.get<Gender[]>(`/api/Lookup/GetGenders`, { headers });
  }
}