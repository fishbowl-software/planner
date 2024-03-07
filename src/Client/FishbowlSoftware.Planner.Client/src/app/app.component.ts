import {Component} from '@angular/core';
import {RouterModule} from '@angular/router';
import {ToastModule} from 'primeng/toast';
import {AuthService, ThemeService} from '@core/services';
import {TopbarComponent, SidebarComponent} from '@layout';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  imports: [
    TopbarComponent,
    SidebarComponent,
    RouterModule,
    ToastModule,
  ],
})
export class AppComponent {
  constructor(
    private readonly authService: AuthService,
    private readonly themeService: ThemeService,
  )
  {
    this.themeService.applyThemeFromStorage();
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
}
