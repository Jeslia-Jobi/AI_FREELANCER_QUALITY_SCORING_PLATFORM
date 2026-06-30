import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { RankingsService } from '../services/rankings';
import { of } from 'rxjs';
import { Rankings } from './rankings';

describe('Rankings', () => {
  let component: Rankings;
  let fixture: ComponentFixture<Rankings>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Rankings],
      providers: [
        provideRouter([]),
        {
          provide: RankingsService,
          useValue: {
            getRankings: () => of([])
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Rankings);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
