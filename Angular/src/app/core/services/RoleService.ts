import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/User';
import { AuthService } from './AuthService';
import { Role } from '../models/Role';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient, private authService: AuthService) {}

  getAllRoles(): Observable<Role[]> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.get<Role[]>(`/api/Roles/GetRoles`, { headers });
  }

  addRole(name: string): Observable<Role> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.post<Role>(`/api/Roles/CreateRole/`+name, name, { headers }).pipe(
        tap((updatedUser: Role) => console.log(`create role`))
      );
  }
  }
