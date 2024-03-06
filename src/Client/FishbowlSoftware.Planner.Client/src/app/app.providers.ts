import {provideHttpClient} from '@angular/common/http';
import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {provideAnimations} from '@angular/platform-browser/animations';
import {provideRouter} from '@angular/router';
import {provideOAuthClient} from 'angular-oauth2-oidc';
import {MessageService} from 'primeng/api';
import {APP_CONFIG} from '@configs';
import {APP_ROUTES} from './app.routes';

export const APP_PROVIDERS: ApplicationConfig = {
  providers: [
    provideRouter(APP_ROUTES),
    provideHttpClient(),
    provideAnimations(),
    provideOAuthClient({
      resourceServer: {
        allowedUrls: [APP_CONFIG.apiUrl],
        sendAccessToken: true,
      }
    }),
    importProvidersFrom(BrowserModule),
    MessageService,
  ],
}
