import {bootstrapApplication} from '@angular/platform-browser';
import {AppComponent} from './app/app.component';
import {AppProviders} from './app/app.providers';

bootstrapApplication(AppComponent, AppProviders).catch(err => {
  console.error(err);
});
