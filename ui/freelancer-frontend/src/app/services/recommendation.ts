import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RecommendationService {

  private apiUrl = 'http://localhost:5029/api/projects';

  constructor(private http: HttpClient) {}

  getRecommendations(projectId: number): Observable<any> {
    return this.http.get(
      `${this.apiUrl}/${projectId}/recommendations`
    );
  }
}