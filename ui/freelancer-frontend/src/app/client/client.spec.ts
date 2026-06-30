import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { Client } from './client';

describe('Client', () => {
  let component: Client;
  let fixture: ComponentFixture<Client>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Client],
      providers: [
        provideRouter([])
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Client);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
