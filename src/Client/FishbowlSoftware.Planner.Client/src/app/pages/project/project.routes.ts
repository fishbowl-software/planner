import {Routes} from '@angular/router';
import {ProjectsListComponent} from './projects-list/projects-list.component';

export const PROJECT_ROUTES: Routes = [
  {
    path: '',
    component: ProjectsListComponent,
    data: { 
      breadcrumb: '' 
    }
  },
];
