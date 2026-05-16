import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Freelancer } from './freelancer';

describe('Freelancer', () => {
  let component: Freelancer;
  let fixture: ComponentFixture<Freelancer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Freelancer],
    }).compileComponents();

    fixture = TestBed.createComponent(Freelancer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
