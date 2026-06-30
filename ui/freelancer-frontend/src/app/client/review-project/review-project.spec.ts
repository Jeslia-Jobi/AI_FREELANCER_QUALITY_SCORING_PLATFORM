import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter,ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { ReviewProject } from './review-project';
import { ReviewService } from '../../services/review';

const reviewServiceMock = {
  getProject: () =>
    of({
      projectId: 1,
      freelancerId: 1
    }),

  submitReview: () => of({})
};

describe('ReviewProject', () => {
  let component: ReviewProject;
  let fixture: ComponentFixture<ReviewProject>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewProject],
      providers: [
        {
          provide: ReviewService,
          useValue: reviewServiceMock
        },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: () => '1'
              }
            }
          }
        },
        provideRouter([])
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ReviewProject);
    component = fixture.componentInstance;

    fixture.detectChanges();
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
