import {CommonModule} from '@angular/common';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {RouterModule} from '@angular/router';
import {Subscription} from 'rxjs';
import {UserDto} from '@core/models';
import {AuthService} from '@core/services';
import {ButtonModule} from 'primeng/button';
import {SidebarModule} from 'primeng/sidebar';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
  imports: [CommonModule, ButtonModule, SidebarModule, RouterModule],
})
export class SidebarComponent implements OnInit, OnDestroy {
  public isMinimized: boolean = false;
  public user: UserDto | null = null;
  private userDataSubscription?: Subscription;

  constructor(private readonly authService: AuthService) {}
  
  ngOnInit(): void {
    this.userDataSubscription = this.authService.userData$.subscribe(userData => {
      this.user = userData;
    });
  }

  ngOnDestroy(): void {
    if (this.userDataSubscription) {
      this.userDataSubscription.unsubscribe();
    }
  }

  toggleMinimize() {
    this.isMinimized = !this.isMinimized;
  }

  logout() {
    this.authService.logout();
  }
}
