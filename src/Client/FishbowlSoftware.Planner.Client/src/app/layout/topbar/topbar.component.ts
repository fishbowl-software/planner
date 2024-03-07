import {Component, OnDestroy} from '@angular/core';
import {Subscription} from 'rxjs';
import {MenuItem} from 'primeng/api';
import {BreadcrumbModule} from 'primeng/breadcrumb';
import {BreadcrumbService} from '@core/services';
import {ThemeSwitcherComponent} from '@shared/components';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.scss',
  standalone: true,
  imports: [BreadcrumbModule, ThemeSwitcherComponent],
})
export class TopbarComponent implements OnDestroy {
  public readonly home: MenuItem;
  public menuItems: MenuItem[] = [];
  private breadcrumbSubscription: Subscription;

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
