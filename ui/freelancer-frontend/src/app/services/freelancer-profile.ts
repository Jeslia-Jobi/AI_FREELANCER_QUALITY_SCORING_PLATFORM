import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FreelancerProfileService {

  apiUrl = 'http://localhost:5029/api/FreelancerProfile';

  constructor(private http: HttpClient) { }

  createProfile(data: any) {
    return this.http.post(this.apiUrl, data);
  }

  getProfile() {
    return this.http.get(this.apiUrl);
  }

}