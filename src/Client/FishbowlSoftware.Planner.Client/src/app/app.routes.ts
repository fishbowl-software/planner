import {Routes} from '@angular/router';
import {Error404Component} from '@pages/error404';
import {UnauthorizedComponent} from '@pages/unauthorized';
import {LoginComponent} from '@pages/login';

export const APP_ROUTES: Routes = [
  {
    path: '',
    redirectTo: 'clients',
    pathMatch: 'full',
  },
  {
    path: 'clients',
    loadChildren: () => import('./pages/client').then(m => m.CLIENT_ROUTES),
    data: { 
      breadcrumb: 'Clients' 
    }
  },
  {
    path: 'projects',
    loadChildren: () => import('./pages/project').then(m => m.PROJECT_ROUTES),
    data: { 
      breadcrumb: 'Projects' 
    }
  },
  {
    path: 'applications',
    loadChildren: () => import('./pages/applications').then(m => m.APPLICATION_ROUTES),
    data: { 
      breadcrumb: 'Applications' 
    }
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
  },
  {
    path: '404',
    component: Error404Component,
  },
  {
    path: '**',
    redirectTo: '404',
  },
];
