import {Routes} from '@angular/router';
import {authGuard} from '@core/guards';
import {ApplicationsListComponent} from './applications-list/applications-list.component';
import {AddApplicationComponent} from './add-application/add-application.component';
import {EditApplicationComponent} from './edit-application/edit-application.component';

export const ApplicationRoutes: Routes = [
  {
    path: '',
    component: ApplicationsListComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: '' 
    },
  },
  {
    path: 'add',
    component: AddApplicationComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: 'Add' 
    }
  },
  {
    path: 'edit/:id',
    component: EditApplicationComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: 'Edit' 
    }
  },
];
