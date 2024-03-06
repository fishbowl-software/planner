import {Routes} from '@angular/router';
import {ApplicationsListComponent} from './applications-list/applications-list.component';

export const APPLICATION_ROUTES: Routes = [
  {
    path: '',
    component: ApplicationsListComponent,
    data: { 
      breadcrumb: '' 
    }
  },
];
