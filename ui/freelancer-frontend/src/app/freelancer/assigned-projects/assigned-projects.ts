import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { ProjectService } from '../../services/project';
import { ReviewService } from '../../services/review';


@Component({
  selector: 'app-assigned-projects',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './assigned-projects.html',
  styleUrl: './assigned-projects.css'
})
export class AssignedProjects implements OnInit {

  projects: any[] = [];
  completedProjects: any[] = [];
  reviews: any[] = [];
  loadingReviews = false;
  reviewsByProject: { [key: number]: any[] } = {};

  constructor(
    private projectService: ProjectService,
    private reviewService: ReviewService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.loadProjects();
    this.loadCompletedProjects();
    this.loadReviews();

  }

  loadProjects() {

    this.projectService
      .getAssignedProjects()
      .subscribe({

        next: (res: any) => {

          console.log(
            'ASSIGNED PROJECTS:',
            res
          );

          this.projects = [...res];

          console.log(
            'PROJECTS VARIABLE:',
            this.projects
          );

          this.cdr.detectChanges();
        },

        error: (err) => {

          console.log(err);

        }
      });
  }

  loadCompletedProjects() {

    this.projectService
      .getCompletedProjects()
      .subscribe({

        next: (res: any) => {
          this.completedProjects = [...res];

          // Re-group reviews after completed projects arrive (in case reviews arrived earlier)
          this.groupReviews();
          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }

      });

  }

  requestCompletion(
    projectId: number
  ) {

    this.projectService
      .requestCompletion(projectId)
      .subscribe({

        next: () => {

          alert(
            'Completion request sent'
          );

          this.loadProjects();
        },

        error: (err) => {

          console.log(err);

        }
      });

  }

  loadReviews() {

    this.loadingReviews = true;

    this.reviewService
      .getMyReviews()
      .subscribe({

        next: (res: any) => {
          console.log('REVIEWS RESPONSE:', res);
          this.reviews = Array.isArray(res) ? res : [];
          // Build grouping (may match by projectId or by title)
          this.groupReviews();
          this.loadingReviews = false;
          this.cdr.detectChanges();
        },

        error: (err) => {
          console.error('REVIEWS LOAD ERROR:', err);
          this.reviews = [];
          this.reviewsByProject = {};
          this.loadingReviews = false;
          this.cdr.detectChanges();
        }

      });

  }

  // Group reviews by projectId. If a review lacks projectId, attempt to match by title
  // against `completedProjects` and assign to that project's id.
  groupReviews() {
    this.reviewsByProject = {};

    if (!Array.isArray(this.reviews)) return;

    for (const r of this.reviews) {
      let id: number | null = null;

      if (r && (r.projectId !== undefined && r.projectId !== null)) {
        id = Number(r.projectId);
      } else if (r && r.title && Array.isArray(this.completedProjects)) {
        const match = this.completedProjects.find(p => String(p.title).trim() === String(r.title).trim());
        if (match) id = Number(match.projectId);
      }

      if (id === null || Number.isNaN(id)) {
        // put in an 'unassigned' bucket with key 0 to avoid losing data
        id = 0;
      }

      if (!this.reviewsByProject[id]) this.reviewsByProject[id] = [];
      this.reviewsByProject[id].push(r);
    }
  }

}