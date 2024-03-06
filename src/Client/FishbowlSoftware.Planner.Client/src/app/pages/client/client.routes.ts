import {Routes} from '@angular/router';
import {authGuard} from '@core/guards';
import {ClientsListComponent} from './clients-list/clients-list.component';

export const CLIENT_ROUTES: Routes = [
  {
    path: '',
    component: ClientsListComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: '' 
    }
  },
];
