import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { ProjectApplicationService }
from '../../services/project-application';

@Component({
  selector: 'app-proposals',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './proposals.html',
  styleUrl: './proposals.css'
})
export class Proposals implements OnInit {

  projects: any[] = [];

  constructor(
    private applicationService: ProjectApplicationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.loadApplications();
  }

  loadApplications() {

    this.applicationService
      .getMyProjectApplications()
      .subscribe({

        next: (res: any) => {

          this.projects = [...res];
          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }
      });
  }

  accept(applicationId: number) {

    this.applicationService
      .acceptApplication(applicationId)
      .subscribe({

        next: () => {

          alert('Freelancer Assigned');

          this.loadApplications();
        },

        error: (err) => {
          console.log(err);
        }
      });
  }
}
