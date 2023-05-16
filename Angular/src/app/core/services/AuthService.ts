import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import * as moment from "moment";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {}


    login(emailAddress: string, password: string): Observable<any> {
        const body = { emailAddress, password };
        return this.http.post(`/api/Authenticate/SignIn`, body).pipe(
            tap((response: any) => {
                localStorage.setItem('token', response.token);
                localStorage.setItem('expires_at', response.expiresIn);
                localStorage.setItem('role', response.role);
            })
        );
    }

    getUserRoles(): Observable<any> {
        const headers = new HttpHeaders({
            Authorization: this.getAuthorizationHeader()
          });
        return this.http.get(`/api/Profile/GetRoles` ,{headers}).pipe(
            tap((response: any) => {
                return response;
            })
        );
    }

    logout(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('expires_at');
        localStorage.removeItem('role');
    }

    signedIn(): Observable<boolean> {
        var isexp = moment().isBefore(this.getExpiration());
        return of(isexp);
    }

    getAuthorizationHeader(): string {
        const token = localStorage.getItem('token');
        return `Bearer ${token}`;
    }

    getExpiration() {
        const expiration = localStorage.getItem("expires_at")!;
        const expiresAt = Date.parse(expiration);
        return moment(expiresAt);
    } 
}