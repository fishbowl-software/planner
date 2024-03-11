import {Routes} from '@angular/router';
import {authGuard} from '@core/guards';
import {ProjectsListComponent} from './projects-list/projects-list.component';
import {AddProjectComponent} from './add-project/add-project.component';
import {EditProjectComponent} from './edit-project/edit-project.component';

export const ProjectRoutes: Routes = [
  {
    path: '',
    component: ProjectsListComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: '' 
    }
  },
  {
    path: 'add',
    component: AddProjectComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: 'Add' 
    }
  },
  {
    path: 'edit/:id',
    component: EditProjectComponent,
    canActivate: [authGuard],
    data: { 
      breadcrumb: 'Edit' 
    }
  },
];
