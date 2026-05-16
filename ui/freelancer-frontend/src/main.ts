import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app';

import { provideHttpClient,withInterceptors } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { authInterceptor } from './app/auth-interceptor';

import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';

provideHttpClient(withInterceptors([authInterceptor]))

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    provideHttpClient(withInterceptors([authInterceptor])),
    importProvidersFrom(FormsModule),
    provideRouter(routes)
  ]
}).catch(err => console.error(err));