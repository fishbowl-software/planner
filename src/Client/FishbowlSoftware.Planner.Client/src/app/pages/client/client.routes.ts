import {Routes} from '@angular/router';
import {authGuard} from '@core/guards';
import {ClientsListComponent} from './clients-list/clients-list.component';
import {AddClientComponent} from './add-client/add-client.component';
import {EditClientComponent} from './edit-client/edit-client.component';

export const ClientRoutes: Routes = [
  {
    path: '',
    component: ClientsListComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: '' 
    }
  },
  {
    path: 'add',
    component: AddClientComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: 'Add' 
    }
  },
  {
    path: 'edit/:id',
    component: EditClientComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: 'Edit' 
    }
  },
];
