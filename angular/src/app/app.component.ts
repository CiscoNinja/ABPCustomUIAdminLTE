import {
  ConfigStateService,
  CurrentUserDto,
} from '@abp/ng.core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  // template: `
  //   <app-appheader></app-appheader>
  //   <div *ngIf="(currentUser$ | async)?.isAuthenticated;">
  //   <app-appmenu></app-appmenu>
  //   </div>
  //   <router-outlet></router-outlet>
  //   <app-appfooter></app-appfooter>
  //   <app-appsetting></app-appsetting>
  // `,
})
export class AppComponent {
  currentUser$: Observable<CurrentUserDto> = this.configState.getOne$('currentUser');

  constructor(
    private configState: ConfigStateService,
  ) { }
}
