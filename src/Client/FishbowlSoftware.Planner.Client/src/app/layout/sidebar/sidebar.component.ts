import {Component} from '@angular/core';
import {ButtonModule} from 'primeng/button';
import {SidebarModule} from 'primeng/sidebar';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
  imports: [ButtonModule, SidebarModule],
})
export class SidebarComponent {
  visible: boolean = false;

  constructor() {}

  // Method to toggle the sidebar visibility
  toggleSidebar() {
    this.visible = !this.visible;
  }
}
