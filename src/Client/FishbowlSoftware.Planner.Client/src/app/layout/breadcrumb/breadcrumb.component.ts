import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {filter} from 'rxjs';
import {MenuItem} from 'primeng/api';
import {BreadcrumbModule} from 'primeng/breadcrumb';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss'],
  standalone: true,
  imports: [BreadcrumbModule],
})
export class BreadcrumbComponent implements OnInit {
  public readonly home: MenuItem;
  public menuItems: MenuItem[];

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.menuItems = [];
    this.home = {
      icon: 'pi pi-home',
      routerLink: '/',
    };
  }

  public ngOnInit(): void {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.menuItems = this.createBreadcrumbs(this.route.root);
      });
  }

  private createBreadcrumbs(
    route: ActivatedRoute,
    url = '',
    items: MenuItem[] = []
  ): MenuItem[] {
    if (!route) {
      return items;
    }

    for (const child of route.children) {
      const routeURL = child.snapshot.url
        .map(segment => segment.path)
        .join('/');
      if (routeURL !== '') {
        url += `/${routeURL}`;
      }

      const label = child.snapshot.data['breadcrumb'] as string;

      if (label) {
        items.push({label, routerLink: url});
      }

      this.createBreadcrumbs(child, url, items);
    }

    return items;
  }
}
