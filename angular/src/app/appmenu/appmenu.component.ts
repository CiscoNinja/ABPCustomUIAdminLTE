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
