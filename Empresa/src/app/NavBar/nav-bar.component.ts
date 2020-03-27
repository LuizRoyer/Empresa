import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html'
})
export class NavbarComponent {

    constructor(private router: Router) {    }   
   
}