import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectApplicationService {

  private apiUrl = 'http://localhost:5029/api/ProjectApplications';

  constructor(private http: HttpClient) {}

  apply(projectId: number) {
    return this.http.post(
      `${this.apiUrl}/${projectId}`,
      {}
    );
  }
}