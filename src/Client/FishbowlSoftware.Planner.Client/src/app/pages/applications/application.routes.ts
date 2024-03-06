import {Routes} from '@angular/router';
import {authGuard} from '@core/guards';
import {ApplicationsListComponent} from './applications-list/applications-list.component';

export const ApplicationRoutes: Routes = [
  {
    path: '',
    component: ApplicationsListComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: '' 
    },
  },
];
