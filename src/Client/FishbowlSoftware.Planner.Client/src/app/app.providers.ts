import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {provideRouter} from '@angular/router';
import {provideOAuthClient} from 'angular-oauth2-oidc';
import {MessageService} from 'primeng/api';
import {authInterceptor} from '@core/interceptors';
import {AppRoutes} from './app.routes';

export const AppProviders: ApplicationConfig = {
  providers: [
    provideRouter(AppRoutes),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideOAuthClient(),
    importProvidersFrom(BrowserModule, BrowserAnimationsModule),
    MessageService,
  ],
}
