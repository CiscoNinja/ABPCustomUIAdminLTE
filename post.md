# Customize ABP Angular Application UI with AdminLTE.

## Introduction

In this article, I will demonstrate how to customzize the angular UI for your ABP application using the AdminLTE tempplate.

> I shall be using some of tips and steps from  [ABP documentation](https://docs.abp.io/en/abp/latest/UI/Angular/Component-Replacement). and this [article](https://www.prishusoft.com/blog/Integrate-AdminLTE-theme-to-Angular-Project.html).
The above links don't show into detail how to completely replace the APB Angular UI without distorting the functionality of your app. I am going to take my time and demonstrate to you step by step.

At the end of this article you will come to realise that we have not removed or replaced any componet as the links above may suggest. 
We are only *Literaly* serving the AdminLTE UI on top of our angular UI.

### Screenshots

Here, the pages of the final application.

**Home Page - Event List**

![event-list-ui](images/event-list-ui.png)

**Creating a new Event**

![event-create-ui](images/event-create-ui.png)

**Event Detail Page**

![event-detail-ui](images/event-detail-ui.png)

## Requirements

* Download the latest release of [Admin LTE](https://github.com/ColorlibHQ/AdminLTE/releases). from this link.
* Create a new ABP Project with Angular UI from [here](https://abp.io/get-started). or from the command line
* Build and run the application to make sure everthing works.

## Customizing the Angular UI

### Step 1 - Setup 

From the Admin LTE Code copy the **dist** and **plugins** folder to *assets* or *node_modules* folder of the angular project.
> I will be using the assets folder

* add this code to the *angular.json* file found in the root of the angular folder 

````json
"styles": [
  {
    "input": "src/assets/dist/css/adminlte.min.css",
    "inject": true,
    "bundleName": "adminlte.min"
  },
 "src/styles.scss"],
 "scripts": [
    "src/assets/plugins/jquery/jquery.min.js",
    "src/assets/plugins/bootstrap/js/bootstrap.bundle.min.js",
    "src/assets/dist/js/adminlte.min.js"
  ]
````

### Step 2 - Add Header Components

* Run the following command in `angular` folder to create a new component called `AppHeaderComponent`. We will use this to replace the default header

````
yarn ng generate component appheader
````

* Open the `appheader.component.ts` file and paste the following lines of code.
> part these lines of code are use to add the ABP `Admin` and `Language` nav items to the top nav [ABP Replace NavItemsComponent](https://docs.abp.io/en/abp/latest/UI/Angular/Component-Replacement#how-to-replace-navitemscomponent).

````ts
import {
  AuthService,
  ConfigStateService,
  CurrentUserDto,
  LanguageInfo,
  NAVIGATE_TO_MANAGE_PROFILE,
  SessionStateService,
} from '@abp/ng.core';
import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
// import snq from 'snq';
// declare var snq: any;

@Component({
  selector: 'app-appheader',
  templateUrl: './appheader.component.html',
  styleUrls: ['./appheader.component.scss']
})
export class AppheaderComponent {
  currentUser$: Observable<CurrentUserDto> = this.configState.getOne$('currentUser');
  selectedTenant$ = this.sessionState.getTenant$();

  languages$: Observable<LanguageInfo[]> = this.configState.getDeep$('localization.languages');

  get smallScreen(): boolean {
    return window.innerWidth < 992;
  }

  get defaultLanguage$(): Observable<string> {
    return this.languages$.pipe(
      map(
        languages =>
          snq(
            () => languages.find(lang => lang.cultureName === this.selectedLangCulture).displayName
          ),
        ''
      )
    );
  }

  get dropdownLanguages$(): Observable<LanguageInfo[]> {
    return this.languages$.pipe(
      map(
        languages =>
          snq(() => languages.filter(lang => lang.cultureName !== this.selectedLangCulture)),
        []
      )
    );
  }

  get selectedLangCulture(): string {
    return this.sessionState.getLanguage();
  }

  constructor(
    @Inject(NAVIGATE_TO_MANAGE_PROFILE) public navigateToManageProfile,
    private configState: ConfigStateService,
    private authService: AuthService,
    private sessionState: SessionStateService
  ) {}

  onChangeLang(cultureName: string) {
    this.sessionState.setLanguage(cultureName);
  }

  navigateToLogin() {
    this.authService.navigateToLogin();
  }

  logout() {
    this.authService.logout().subscribe();
  }
}

function snq(arg0: () => any): any {
}


````

* Open the `appheader.component.html` file and paste the following lines of code. These lines add the Top Navigation Elements of the AdminLTE template to our UI

````html
    <nav class="main-header navbar navbar-expand navbar-white navbar-light">
        <ul class="navbar-nav">
          <li class="nav-item">
            <a class="nav-link" data-widget="pushmenu" href="#" role="button"><i class="fas fa-bars"></i></a>
          </li>
          <li class="nav-item d-none d-sm-inline-block">
            <a href="index3.html" class="nav-link">Home</a>
          </li>
          <li class="nav-item d-none d-sm-inline-block">
            <a href="#" class="nav-link">Contact</a>
          </li>
        </ul>
    
        <ul class="navbar-nav ml-auto">
          <li class="nav-item dropdown">
            <a class="nav-link" data-toggle="dropdown" href="#">
              <i class="far fa-comments"></i>
              <span class="badge badge-danger navbar-badge">3</span>
            </a>
            <div class="dropdown-menu dropdown-menu-lg dropdown-menu-right">
              <a href="#" class="dropdown-item">
                <div class="media">
                  <img src="assets/dist/img/user1-128x128.jpg" alt="User Avatar" class="img-size-50 mr-3 img-circle">
                  <div class="media-body">
                    <h3 class="dropdown-item-title">
                      Brad Diesel
                      <span class="float-right text-sm text-danger"><i class="fas fa-star"></i></span>
                    </h3>
                    <p class="text-sm">Call me whenever you can...</p>
                    <p class="text-sm text-muted"><i class="far fa-clock mr-1"></i> 4 Hours Ago</p>
                  </div>
                </div>
              </a>
              <div class="dropdown-divider"></div>
              <a href="#" class="dropdown-item">
                <div class="media">
                  <img src="assets/dist/img/user8-128x128.jpg" alt="User Avatar" class="img-size-50 img-circle mr-3">
                  <div class="media-body">
                    <h3 class="dropdown-item-title">
                      John Pierce
                      <span class="float-right text-sm text-muted"><i class="fas fa-star"></i></span>
                    </h3>
                    <p class="text-sm">I got your message bro</p>
                    <p class="text-sm text-muted"><i class="far fa-clock mr-1"></i> 4 Hours Ago</p>
                  </div>
                </div>
              </a>
              <div class="dropdown-divider"></div>
              <a href="#" class="dropdown-item">
                <div class="media">
                  <img src="assets/dist/img/user3-128x128.jpg" alt="User Avatar" class="img-size-50 img-circle mr-3">
                  <div class="media-body">
                    <h3 class="dropdown-item-title">
                      Nora Silvester
                      <span class="float-right text-sm text-warning"><i class="fas fa-star"></i></span>
                    </h3>
                    <p class="text-sm">The subject goes here</p>
                    <p class="text-sm text-muted"><i class="far fa-clock mr-1"></i> 4 Hours Ago</p>
                  </div>
                </div>
              </a>
              <div class="dropdown-divider"></div>
              <a href="#" class="dropdown-item dropdown-footer">See All Messages</a>
            </div>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link" data-toggle="dropdown" href="#">
              <i class="far fa-bell"></i>
              <span class="badge badge-warning navbar-badge">15</span>
            </a>
            <div class="dropdown-menu dropdown-menu-lg dropdown-menu-right">
              <span class="dropdown-item dropdown-header">15 Notifications</span>
              <div class="dropdown-divider"></div>
              <a href="#" class="dropdown-item">
                <i class="fas fa-envelope mr-2"></i> 4 new messages
                <span class="float-right text-muted text-sm">3 mins</span>
              </a>
              <div class="dropdown-divider"></div>
              <a href="#" class="dropdown-item">
                <i class="fas fa-users mr-2"></i> 8 friend requests
                <span class="float-right text-muted text-sm">12 hours</span>
              </a>
              <div class="dropdown-divider"></div>
              <a href="#" class="dropdown-item">
                <i class="fas fa-file mr-2"></i> 3 new reports
                <span class="float-right text-muted text-sm">2 days</span>
              </a>
              <div class="dropdown-divider"></div>
              <a href="#" class="dropdown-item dropdown-footer">See All Notifications</a>
            </div>
          </li>
          <li class="nav-item">
            <a class="nav-link" data-widget="fullscreen" href="#" role="button">
              <i class="fas fa-expand-arrows-alt"></i>
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link" data-widget="control-sidebar" data-controlsidebar-slide="true" href="#" role="button">
              <i class="fas fa-th-large"></i>
            </a>
          </li>
        </ul>
      </nav>

````

* And finally add these line of code to the tail or wherever you see fit inside the `<nav></nav>` tag of the `appheader.component.html` file 
> these lines adds the `Admin` and `language` UI dropdows to the nav element

````html
<ul class="navbar-nav">
          <!-- <input type="search" placeholder="Search" class="bg-transparent border-0 text-white" /> -->
          <li class="nav-item d-flex align-items-center">
            <div
              *ngIf="(dropdownLanguages$ | async)?.length > 0"
              class="dropdown"
              ngbDropdown
              #languageDropdown="ngbDropdown"
              display="static"
            >
              <a
                ngbDropdownToggle
                class="nav-link"
                href="javascript:void(0)"
                role="button"
                id="dropdownMenuLink"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                {{ defaultLanguage$ | async }}
              </a>
              <div
                class="dropdown-menu dropdown-menu-right border-0 shadow-sm"
                aria-labelledby="dropdownMenuLink"
                [class.d-block]="smallScreen && languageDropdown.isOpen()"
              >
                <a
                  *ngFor="let lang of dropdownLanguages$ | async"
                  href="javascript:void(0)"
                  class="dropdown-item"
                  (click)="onChangeLang(lang.cultureName)"
                  >{{ lang?.displayName }}</a
                >
              </div>
            </div>
          </li>
          <li class="nav-item d-flex align-items-center">
            <ng-template #loginBtn>
              <a role="button" class="nav-link pointer" (click)="navigateToLogin()">{{
                'AbpAccount::Login' | abpLocalization
              }}</a>
            </ng-template>
            <div
              *ngIf="(currentUser$ | async)?.isAuthenticated; else loginBtn"
              ngbDropdown
              class="dropdown"
              #currentUserDropdown="ngbDropdown"
              display="static"
            >
              <a
                ngbDropdownToggle
                class="nav-link"
                href="javascript:void(0)"
                role="button"
                id="dropdownMenuLink"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                <small *ngIf="(selectedTenant$ | async)?.name as tenantName"
                  ><i>{{ tenantName }}</i
                  >\</small
                >
                <strong>{{ (currentUser$ | async)?.userName }}</strong>
              </a>
              <div
                class="dropdown-menu dropdown-menu-right border-0 shadow-sm"
                aria-labelledby="dropdownMenuLink"
                [class.d-block]="smallScreen && currentUserDropdown.isOpen()"
              >
                <a class="dropdown-item pointer" (click)="navigateToManageProfile()"
                  ><i class="fa fa-cog mr-1"></i>{{ 'AbpAccount::MyAccount' | abpLocalization }}</a
                >
                <a class="dropdown-item" href="javascript:void(0)" (click)="logout()"
                  ><i class="fa fa-power-off mr-1"></i>{{ 'AbpUi::Logout' | abpLocalization }}</a
                >
              </div>
            </div>
          </li>
        </ul>
````

### Step 3 - Add Left/Side Menu Components

* Run the following command in `angular` folder to create a new component called `AppMenuComponent`. This will be our *Side Menu*

````
yarn ng generate component appmenu
````

* Open the `appmenu.component.ts` file and paste the following lines of code.

````ts
import { Component, OnInit } from '@angular/core';
declare var $: any;

@Component({
  selector: 'app-appmenu',
  templateUrl: './appmenu.component.html',
  styleUrls: ['./appmenu.component.scss']
})
export class AppmenuComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    $('[data-widget="treeview"]').Treeview('init'); // inits the collapsibel menus
  }

}
````

* Open the `appmenu.component.html` file and paste the following lines of code.

````html
<aside class="main-sidebar sidebar-dark-primary elevation-4">
  <!-- Brand Logo -->
  <a href="index3.html" class="brand-link">
    <img src="assets/dist/img/AdminLTELogo.png" alt="AdminLTE Logo" class="brand-image img-circle elevation-3"
      style="opacity: .8">
    <span class="brand-text font-weight-light">AdminLTE 3</span>
  </a>

  <!-- Sidebar -->
  <div class="sidebar">
    <!-- Sidebar user panel (optional) -->
    <div class="user-panel mt-3 pb-3 mb-3 d-flex">
      <div class="image">
        <img src="assets/dist/img/user2-160x160.jpg" class="img-circle elevation-2" alt="User Image">
      </div>
      <div class="info">
        <a href="#" class="d-block">Alexander Pierce</a>
      </div>
    </div>

    <!-- SidebarSearch Form -->
    <div class="form-inline">
      <div class="input-group" data-widget="sidebar-search">
        <input class="form-control form-control-sidebar" type="search" placeholder="Search" aria-label="Search">
        <div class="input-group-append">
          <button class="btn btn-sidebar">
            <i class="fas fa-search fa-fw"></i>
          </button>
        </div>
      </div>
    </div>

    <!-- Sidebar Menu -->
    <nav class="mt-2">
      <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
        <li class="nav-item">
          <a routerLink="/" class="nav-link">
            <i class="nav-icon fas fa-th"></i>
            <p>
              {{ '::Menu:Home' | abpLocalization }}
            </p>
          </a>
        </li>

        <!-- Add icons to the links using the .nav-icon class
               with font-awesome or any other icon font library -->
        <li class="nav-item menu-open" *abpPermission="'AbpIdentity.Roles || AbpIdentity.Users'">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-tachometer-alt"></i>
            <p>
              {{ 'AbpIdentity::Menu:IdentityManagement' | abpLocalization }}
              <i class="right fas fa-angle-left"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item" *abpPermission="'AbpIdentity.Roles'">
              <a routerLink="/identity/roles" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                {{ 'AbpIdentity::Roles' | abpLocalization }}
              </a>
            </li>
            <li class="nav-item" *abpPermission="'AbpIdentity.Users'">
              <a routerLink="/identity/users" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                {{ 'AbpIdentity::Users' | abpLocalization }}
              </a>
            </li>
            <!-- <li class="nav-item">
              <a href="./index3.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Dashboard v3</p>
              </a>
            </li> -->
          </ul>
        </li>
        <li class="nav-item menu-open" *abpPermission="'AbpTenantManagement.Tenants'">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-tachometer-alt"></i>
            <p>
              {{ 'AbpTenantManagement::Menu:TenantManagement' | abpLocalization }}
              <i class="right fas fa-angle-left"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a routerLink="/tenant-management/tenants" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                {{ 'AbpTenantManagement::Tenants' | abpLocalization }}
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="pages/widgets.html" class="nav-link">
            <i class="nav-icon fas fa-th"></i>
            <p>
              Widgets
              <span class="right badge badge-danger">New</span>
            </p>
          </a>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-copy"></i>
            <p>
              Layout Options
              <i class="fas fa-angle-left right"></i>
              <span class="badge badge-info right">6</span>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/layout/top-nav.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Top Navigation</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/layout/top-nav-sidebar.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Top Navigation + Sidebar</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/layout/boxed.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Boxed</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/layout/fixed-sidebar.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Fixed Sidebar</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/layout/fixed-sidebar-custom.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Fixed Sidebar <small>+ Custom Area</small></p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/layout/fixed-topnav.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Fixed Navbar</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/layout/fixed-footer.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Fixed Footer</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/layout/collapsed-sidebar.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Collapsed Sidebar</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-chart-pie"></i>
            <p>
              Charts
              <i class="right fas fa-angle-left"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/charts/chartjs.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>ChartJS</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/charts/flot.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Flot</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/charts/inline.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Inline</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/charts/uplot.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>uPlot</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-tree"></i>
            <p>
              UI Elements
              <i class="fas fa-angle-left right"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/UI/general.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>General</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/UI/icons.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Icons</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/UI/buttons.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Buttons</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/UI/sliders.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Sliders</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/UI/modals.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Modals & Alerts</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/UI/navbar.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Navbar & Tabs</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/UI/timeline.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Timeline</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/UI/ribbons.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Ribbons</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-edit"></i>
            <p>
              Forms
              <i class="fas fa-angle-left right"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/forms/general.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>General Elements</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/forms/advanced.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Advanced Elements</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/forms/editors.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Editors</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/forms/validation.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Validation</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-table"></i>
            <p>
              Tables
              <i class="fas fa-angle-left right"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/tables/simple.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Simple Tables</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/tables/data.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>DataTables</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/tables/jsgrid.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>jsGrid</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-header">EXAMPLES</li>
        <li class="nav-item">
          <a href="pages/calendar.html" class="nav-link">
            <i class="nav-icon far fa-calendar-alt"></i>
            <p>
              Calendar
              <span class="badge badge-info right">2</span>
            </p>
          </a>
        </li>
        <li class="nav-item">
          <a href="pages/gallery.html" class="nav-link">
            <i class="nav-icon far fa-image"></i>
            <p>
              Gallery
            </p>
          </a>
        </li>
        <li class="nav-item">
          <a href="pages/kanban.html" class="nav-link">
            <i class="nav-icon fas fa-columns"></i>
            <p>
              Kanban Board
            </p>
          </a>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon far fa-envelope"></i>
            <p>
              Mailbox
              <i class="fas fa-angle-left right"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/mailbox/mailbox.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Inbox</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/mailbox/compose.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Compose</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/mailbox/read-mail.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Read</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-book"></i>
            <p>
              Pages
              <i class="fas fa-angle-left right"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/examples/invoice.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Invoice</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/profile.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Profile</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/e-commerce.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>E-commerce</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/projects.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Projects</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/project-add.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Project Add</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/project-edit.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Project Edit</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/project-detail.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Project Detail</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/contacts.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Contacts</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/faq.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>FAQ</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/contact-us.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Contact us</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon far fa-plus-square"></i>
            <p>
              Extras
              <i class="fas fa-angle-left right"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="#" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>
                  Login & Register v1
                  <i class="fas fa-angle-left right"></i>
                </p>
              </a>
              <ul class="nav nav-treeview">
                <li class="nav-item">
                  <a href="pages/examples/login.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Login v1</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="pages/examples/register.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Register v1</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="pages/examples/forgot-password.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Forgot Password v1</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="pages/examples/recover-password.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Recover Password v1</p>
                  </a>
                </li>
              </ul>
            </li>
            <li class="nav-item">
              <a href="#" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>
                  Login & Register v2
                  <i class="fas fa-angle-left right"></i>
                </p>
              </a>
              <ul class="nav nav-treeview">
                <li class="nav-item">
                  <a href="pages/examples/login-v2.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Login v2</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="pages/examples/register-v2.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Register v2</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="pages/examples/forgot-password-v2.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Forgot Password v2</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="pages/examples/recover-password-v2.html" class="nav-link">
                    <i class="far fa-circle nav-icon"></i>
                    <p>Recover Password v2</p>
                  </a>
                </li>
              </ul>
            </li>
            <li class="nav-item">
              <a href="pages/examples/lockscreen.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Lockscreen</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/legacy-user-menu.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Legacy User Menu</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/language-menu.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Language Menu</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/404.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Error 404</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/500.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Error 500</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/pace.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Pace</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/examples/blank.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Blank Page</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="starter.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Starter Page</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-search"></i>
            <p>
              Search
              <i class="fas fa-angle-left right"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="pages/search/simple.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Simple Search</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="pages/search/enhanced.html" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Enhanced</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-header">MISCELLANEOUS</li>
        <li class="nav-item">
          <a href="iframe.html" class="nav-link">
            <i class="nav-icon fas fa-ellipsis-h"></i>
            <p>Tabbed IFrame Plugin</p>
          </a>
        </li>
        <li class="nav-item">
          <a href="https://adminlte.io/docs/3.1/" class="nav-link">
            <i class="nav-icon fas fa-file"></i>
            <p>Documentation</p>
          </a>
        </li>
        <li class="nav-header">MULTI LEVEL EXAMPLE</li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="fas fa-circle nav-icon"></i>
            <p>Level 1</p>
          </a>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon fas fa-circle"></i>
            <p>
              Level 1
              <i class="right fas fa-angle-left"></i>
            </p>
          </a>
          <ul class="nav nav-treeview">
            <li class="nav-item">
              <a href="#" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Level 2</p>
              </a>
            </li>
            <li class="nav-item">
              <a href="#" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>
                  Level 2
                  <i class="right fas fa-angle-left"></i>
                </p>
              </a>
              <ul class="nav nav-treeview">
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="far fa-dot-circle nav-icon"></i>
                    <p>Level 3</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="far fa-dot-circle nav-icon"></i>
                    <p>Level 3</p>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="far fa-dot-circle nav-icon"></i>
                    <p>Level 3</p>
                  </a>
                </li>
              </ul>
            </li>
            <li class="nav-item">
              <a href="#" class="nav-link">
                <i class="far fa-circle nav-icon"></i>
                <p>Level 2</p>
              </a>
            </li>
          </ul>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="fas fa-circle nav-icon"></i>
            <p>Level 1</p>
          </a>
        </li>
        <li class="nav-header">LABELS</li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon far fa-circle text-danger"></i>
            <p class="text">Important</p>
          </a>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon far fa-circle text-warning"></i>
            <p>Warning</p>
          </a>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link">
            <i class="nav-icon far fa-circle text-info"></i>
            <p>Informational</p>
          </a>
        </li>
      </ul>
    </nav>
    <!-- /.sidebar-menu -->
  </div>
  <!-- /.sidebar -->
</aside>
````

### Step 4 - Bringing it all together

* Open the `index.html` in the root folder file and and modify as follows.

````html
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>CustomUIAdminLTE</title>
  <base href="/" />

  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link rel="icon" type="image/x-icon" href="favicon.ico" />
  <!-- Bootstrap 3.3.7 -->
  <!-- Google Font: Source Sans Pro -->
    
</head>

<body class="hold-transition sidebar-mini layout-fixed">

  <div class="wrapper">
    <!-- adds the preloader IconPreloader -->
    <div class="preloader flex-column justify-content-center align-items-center">
      <img class="animation__shake" src="/assets/dist/img/AdminLTELogo.png" alt="LoaderLogo" height="60" width="60">
    </div>
    <app-root>
    </app-root>
  </div>

</body>

</html>
````

* Open the `app.module.ts` file.
* Import the `SharedModule` to the `imports` array of `AppModule`

````ts

import { SharedModule } from './shared/shared.module';

@NgModule({
  imports: [
    //...
    SharedModule
  ]
)}

</Row>
````

* Open the `app.component.ts` file.
* Replace the content with the following code:

````ts
import {
  ConfigStateService,
  CurrentUserDto,
} from '@abp/ng.core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  currentUser$: Observable<CurrentUserDto> = this.configState.getOne$('currentUser');

  constructor(
    private configState: ConfigStateService,
  ) { }
}

````

* Create and open the `app.component.html` file.
* Replace the content with the following code:

````html
<div *ngIf="(currentUser$ | async)?.isAuthenticated; else noSidebar">
    <app-appheader></app-appheader>
    <app-appmenu></app-appmenu>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <router-outlet></router-outlet>
            </div>
        </section>
    </div>
    <app-appfooter></app-appfooter>
    <app-appsetting></app-appsetting>
    
</div>
<!-- to make sure sidebar is hidden when logged out-->
<ng-template #noSidebar>
    <router-outlet></router-outlet>
  </ng-template>
````


## Source Code

Source code of the completed application is [available on GitHub](https://github.com/CiscoNinja/ABPCustomUIAdminLTE).
