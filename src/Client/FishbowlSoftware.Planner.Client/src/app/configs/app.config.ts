import {ApplicationConfig} from '@angular/core';
import {provideRouter} from '@angular/router';
import {provideHttpClient} from '@angular/common/http';
import {provideOAuthClient} from 'angular-oauth2-oidc';
import {APP_ROUTES} from '../app.routes';

export const APP_CONFIG: ApplicationCustomConfig = {
  apiUrl: 'https://localhost:7000',
  providers: [
    provideRouter(APP_ROUTES),
    provideHttpClient(),
    provideOAuthClient({
      resourceServer: {
        allowedUrls: ['https://localhost:7000'],
        sendAccessToken: true,
      }
    }),
  ],
};

interface ApplicationCustomConfig extends ApplicationConfig {
  apiUrl: string;
}
