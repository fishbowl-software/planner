import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, NavigationEnd, Router} from '@angular/router';
import {BehaviorSubject} from 'rxjs';
import {filter} from 'rxjs/operators';
import {MenuItem} from 'primeng/api';

@Injectable({providedIn: 'root'})
export class BreadcrumbService {
  private readonly breadcrumbsSubject = new BehaviorSubject<MenuItem[]>([]);
  public readonly breadcrumbs$ = this.breadcrumbsSubject.asObservable();

  constructor(private readonly router: Router) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        // Dynamically generate breadcrumb items
        const root = this.router.routerState.snapshot.root;
        const breadcrumbs: MenuItem[] = [];
        this.addBreadcrumbItem(root, [], breadcrumbs);
        this.breadcrumbsSubject.next(breadcrumbs);
      });
  }

  private addBreadcrumbItem(
    route: ActivatedRouteSnapshot,
    parentUrl: string[],
    breadcrumbs: MenuItem[]
  )
  {
    if (
      route.routeConfig &&
      route.routeConfig.path &&
      route.routeConfig.data &&
      route.routeConfig.data['breadcrumb']
    )
    {
      const routeUrl = parentUrl.concat(route.routeConfig.path);
      breadcrumbs.push({
        label: route.routeConfig.data['breadcrumb'],
        routerLink: [routeUrl.join('/')],
      });
    }

    if (route.firstChild) {
      this.addBreadcrumbItem(
        route.firstChild,
        parentUrl.concat(route.routeConfig?.path ? route.routeConfig.path : []),
        breadcrumbs
      );
    }
  }
}
