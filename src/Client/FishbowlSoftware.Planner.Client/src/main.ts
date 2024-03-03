import {bootstrapApplication} from '@angular/platform-browser';
import {APP_CONFIG} from '@configs';
import {AppComponent} from './app/app.component';

bootstrapApplication(AppComponent, APP_CONFIG).catch(err => {
  console.error(err);
});
