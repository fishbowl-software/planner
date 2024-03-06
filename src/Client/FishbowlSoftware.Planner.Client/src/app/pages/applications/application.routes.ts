import {Routes} from '@angular/router';
import {authGuard} from '@core/guards';
import {ApplicationsListComponent} from './applications-list/applications-list.component';

export const APPLICATION_ROUTES: Routes = [
  {
    path: '',
    component: ApplicationsListComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: '' 
    },
  },
];
