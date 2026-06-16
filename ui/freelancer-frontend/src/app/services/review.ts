import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private apiUrl =
    'http://localhost:5029/api/projects';

  constructor(
    private http: HttpClient
  ) {}

  submitReview(data: any) {

    return this.http.post(
      `${this.apiUrl}/review`,
      data
    );

  }

  getProject(id: number) {

    return this.http.get(
      `${this.apiUrl}/${id}`
    );

  }

  getMyReviews() {
    return this.http.get(
      'http://localhost:5029/api/reviews/my-reviews'
    );
  }

}