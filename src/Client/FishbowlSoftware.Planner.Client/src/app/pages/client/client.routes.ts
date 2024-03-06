import {Routes} from '@angular/router';
import {ClientsListComponent} from './clients-list/clients-list.component';

export const CLIENT_ROUTES: Routes = [
  {
    path: '',
    component: ClientsListComponent,
    data: { 
      breadcrumb: '' 
    }
  },
];
