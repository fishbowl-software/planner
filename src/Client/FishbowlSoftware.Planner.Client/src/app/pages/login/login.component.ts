import {Component, OnDestroy, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Subscription} from 'rxjs';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {ButtonModule} from 'primeng/button';
import {AuthService} from '@core/services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  imports: [
    ProgressSpinnerModule,
    ButtonModule,
  ],
})
export class LoginComponent implements OnInit, OnDestroy {
  private userDataSubscription?: Subscription;
  public isLoading = false;

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router
  )
  {
  }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
  
  ngOnInit(): void {
    this.userDataSubscription = this.authService.userData$
      .subscribe(userData => {
        if (userData) {
          this.router.navigateByUrl('/');
        }
      });
  }

  ngOnDestroy(): void {
    if (this.userDataSubscription) {
      this.userDataSubscription.unsubscribe();
    }
  }

  login() {
    this.isLoading = true;
    this.authService.login();
  }
}
