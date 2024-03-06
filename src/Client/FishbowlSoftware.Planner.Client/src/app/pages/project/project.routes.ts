import {Routes} from '@angular/router';
import {authGuard} from '@core/guards';
import {ProjectsListComponent} from './projects-list/projects-list.component';

export const ProjectRoutes: Routes = [
  {
    path: '',
    component: ProjectsListComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: '' 
    }
  },
];
