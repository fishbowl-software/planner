import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {OAuthService} from 'angular-oauth2-oidc';
import {filter} from 'rxjs';
import {AUTH_CONFIG} from '@configs';
import {ButtonModule} from 'primeng/button';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  imports: [ButtonModule, RouterOutlet],
})
export class AppComponent {
  constructor(private readonly oauthService: OAuthService) {
    this.oauthService.configure(AUTH_CONFIG);
    this.oauthService.loadDiscoveryDocumentAndLogin();
    this.oauthService.setupAutomaticSilentRefresh();

    // Automatically load user profile
    this.oauthService.events
      .pipe(filter(e => e.type === 'token_received'))
      .subscribe(() => this.oauthService.loadUserProfile());
  }

  get userName(): string | null {
    const claims = this.oauthService.getIdentityClaims();

    if (!claims) {
      return null;
    }

    return claims['name'];
  }

  get idToken(): string {
    return this.oauthService.getIdToken();
  }

  get accessToken(): string {
    return this.oauthService.getAccessToken();
  }
}
