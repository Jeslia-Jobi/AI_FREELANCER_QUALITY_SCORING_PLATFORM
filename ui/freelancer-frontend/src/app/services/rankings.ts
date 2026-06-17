import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RankingsService {

  private apiUrl =
    'http://localhost:5029/api/rankings';

  constructor(
    private http: HttpClient
  ) {}

  getRankings() {
    return this.http.get(this.apiUrl);
  }
}