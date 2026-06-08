import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  apiUrl = 'http://localhost:5029/api/projects';

  constructor(private http: HttpClient) { }

  createProject(data:any){
    return this.http.post(this.apiUrl, data);
  }

  getMyProjects(){
    return this.http.get(this.apiUrl + '/my-projects');
  }

  getAllProjects(){
    return this.http.get(this.apiUrl);
  }

  getAssignedProjects() {

    const token = localStorage.getItem('token');

    return this.http.get(
      this.apiUrl + '/assigned',
      {
        headers: new HttpHeaders({
          Authorization: `Bearer ${token}`
        })
      }
    );
  }
}