import { Component, OnInit, Input } from '@angular/core';
import {Location, LocationStrategy, PathLocationStrategy} from '@angular/common';

export class TopMenu{
	name: string;
	rusName: string;
}

@Component({
  selector: 'app-top-menu',
  providers: [Location, {provide: LocationStrategy, useClass: PathLocationStrategy}],
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})


export class TopMenuComponent implements OnInit {
	
  items: TopMenu[] = [
	  {name: "Information", rusName: "Информация"}, 
//	  {name: "Finance", rusName: "Расписание"}, 
//	  {name: "Administration", rusName: "Администрирование"}
  ];
  
  @Input() selectBlock: string;
  $location: Location;
  
  constructor($location: Location) {
    this.$location = $location;
  }

  ngOnInit() {
  } 
  
  replace(name: string){
	  this.$location.go(name);
	  this.selectBlock = name;
  }

}
