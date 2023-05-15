import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from './AuthService';
import { Project } from '../models/Project';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient, private authService: AuthService) {}

  getAllProjects(): Observable<Project[]> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.get<Project[]>(`/api/Projects/GetProjects`, { headers });
  }

  createUser(project: Project): Observable<Project> {
    const headers = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeader()
    });
    return this.http.post<Project>(`/api/Projects/CreateProject`, project, { headers }).pipe(
      tap((createdProject: Project) => console.log(`Created project ${createdProject.id}`))
    );
  }
}