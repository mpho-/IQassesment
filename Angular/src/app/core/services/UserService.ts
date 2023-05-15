import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/User';
import { AuthService } from './AuthService';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private authService: AuthService) {}

  getAllUsers(): Observable<User[]> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.get<User[]>(`/api/Administration/GetUsers`, { headers });
  }

  updateUser(user: User): Observable<User> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.put<User>(`/api/Administration/UpdateUser`, user, { headers }).pipe(
        tap((updatedUser: User) => console.log(`Updated user ${updatedUser.id}`))
      );
    }

  getCurrentUser(): Observable<User> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.get<User>(`/api/Profile/Get`, { headers });
  }

  createUser(user: User): Observable<User> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.post<User>(`/api/Administration/CreateUser`, user, { headers }).pipe(
      tap((createdUser: User) => console.log(`Created user ${createdUser.id}`))
    );
  }
  
  deleteUser(user: User): Observable<User> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.delete<User>(`/api/Administration/DeleteUser/${user.id}`, { headers }).pipe(
      tap(() => console.log(`Deleted user ${user.id}`))
    );
  }

  getUserById(id: number): Observable<User> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.get<User>(`/api/Administration/GetUserById/`+id, { headers }).pipe(
      tap((createdUser: User) => console.log(`retrieved user ${createdUser.id}`))
    );
  }

  uploadById(id: number, fileToUpload: File): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    return this.http.put(`/api/Administration/UploadImage/`+id, formData, { headers }).pipe(
      tap(() => console.log(`image uploaded`))
    );
  }
}