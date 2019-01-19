import { Component } from '@angular/core';
import { NavLink } from './navlink.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  navLinks: NavLink[] = [
    {
      url: '/counter',
      text: 'Test'
    },
    {
      url: '/fetch-data',
      text: 'Fetch data'
    },
  ];

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
