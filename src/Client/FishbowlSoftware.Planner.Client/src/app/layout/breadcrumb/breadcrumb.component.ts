import {Component, OnDestroy} from '@angular/core';
import {Subscription} from 'rxjs';
import {MenuItem} from 'primeng/api';
import {BreadcrumbModule} from 'primeng/breadcrumb';
import {BreadcrumbService} from '@core/services';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  standalone: true,
  imports: [BreadcrumbModule],
})
export class BreadcrumbComponent implements OnDestroy {
  public readonly home: MenuItem;
  private breadcrumbSubscription: Subscription;
  public menuItems: MenuItem[] = [];

  constructor(breadcrumbService: BreadcrumbService) {
    this.home = {
      icon: 'bi bi-house',
      routerLink: '/',
    };

    this.breadcrumbSubscription = breadcrumbService.breadcrumbs$.subscribe(items => {
      this.menuItems = items;
    });
  }

  ngOnDestroy(): void {
    if (this.breadcrumbSubscription) {
      this.breadcrumbSubscription.unsubscribe();
    }
  }
}
