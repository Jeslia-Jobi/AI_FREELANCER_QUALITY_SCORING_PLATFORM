import {Component, OnInit, ChangeDetectorRef} from '@angular/core';

import { CommonModule } from '@angular/common';

import { RouterLink } from '@angular/router';

import { RankingsService } from '../services/rankings';

@Component({
  selector: 'app-rankings',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './rankings.html',
  styleUrl: './rankings.css'
})
export class Rankings implements OnInit {

  rankings: any[] = [];

  constructor(
    private rankingsService: RankingsService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.rankingsService
      .getRankings()
      .subscribe({

        next: (res: any) => {
          // console.log('RANKINGS RECEIVED', res);
          this.rankings = res;
          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }

      });

  }

}