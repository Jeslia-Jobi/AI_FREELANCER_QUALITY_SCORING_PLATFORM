import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectApplicationService {

  private apiUrl =
    'http://localhost:5029/api/ProjectApplications';

  constructor(private http: HttpClient) {}

  // Freelancer applies to a project
  apply(projectId: number) {
    return this.http.post(
      `${this.apiUrl}/${projectId}`,
      {}
    );
  }

  // Client views proposals/applicants
  getMyProjectApplications() {
  return this.http.get(
    `${this.apiUrl}/proposals`
  );
}

  // Client accepts an applicant
  acceptApplication(applicationId: number) {
    return this.http.put(
      `${this.apiUrl}/accept/${applicationId}`,
      {}
    );
  }
}