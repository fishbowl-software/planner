import {CommonModule} from '@angular/common';
import {Component} from '@angular/core';
import {RouterModule} from '@angular/router';
import {ButtonModule} from 'primeng/button';
import {SidebarModule} from 'primeng/sidebar';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
  imports: [CommonModule, ButtonModule, SidebarModule, RouterModule],
})
export class SidebarComponent {
  public isMinimized: boolean = false;

  constructor() {}

  toggleMinimize() {
    this.isMinimized = !this.isMinimized;
  }
}
