import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {ToastModule} from 'primeng/toast';
import {AuthService} from '@core/services';
import {BreadcrumbComponent, SidebarComponent} from '@layout';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  imports: [
    BreadcrumbComponent,
    SidebarComponent,
    RouterOutlet,
    ToastModule,
  ],
})
export class AppComponent {
  constructor(private readonly authService: AuthService) {}

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
}
