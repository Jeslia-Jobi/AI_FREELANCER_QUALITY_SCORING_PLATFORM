import {
  Component,
  OnInit
} from '@angular/core';

import {
  ActivatedRoute,
  Router
} from '@angular/router';

import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ReviewService }
from '../../services/review';

@Component({
  selector: 'app-review-project',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './review-project.html',
  styleUrl: './review-project.css'
})
export class ReviewProject
implements OnInit {

  project: any;

  review = {
    projectId: 0,
    freelancerId: 0,
    rating: 5,
    feedback: ''
  };

  constructor(
    private route:
      ActivatedRoute,
    private reviewService:
      ReviewService,
    private router: Router
  ) {}

  ngOnInit(): void {

    const projectId =
      Number(
        this.route.snapshot.paramMap
        .get('id')
      );

    this.reviewService
      .getProject(projectId)
      .subscribe({

        next: (data: any) => {

          this.project = data;

          this.review.projectId =
            data.projectId;

          this.review.freelancerId =
            data.freelancerId;
        }

      });

  }

  submitReview() {

    this.reviewService
      .submitReview(this.review)
      .subscribe({

        next: () => {

          alert(
            'Review submitted'
          );

          this.router.navigate(
            ['/proposals']
          );

        }

      });

  }

}