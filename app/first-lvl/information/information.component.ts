import { Component, OnInit } from '@angular/core';

import {MenuService, Menu} from './menu.service'
import { Observable, of} from 'rxjs';

@Component({
  selector: 'app-information',
  templateUrl: './information.component.html',
  styleUrls: ['./information.component.css']
})
export class InformationComponent implements OnInit {
  blockName: string = "Information";
  menu : Menu[];
  
  constructor(
  private menuServ: MenuService
  ) { }

  ngOnInit() {
	  this.menuServ.get()
	    .subscribe((data: Menu[]) => this.menu = data);
  }

}
