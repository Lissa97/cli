import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FirstLvlRoutingModule } from './first-lvl-routing.module';
import { FirstLvlComponent } from './first-lvl.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { InformationComponent } from './information/information.component';

import { ClarityModule } from '@clr/angular';
import { HttpClientModule }   from '@angular/common/http';
import { AuthComponent } from './auth/auth.component';
 
@NgModule({
  declarations: [
    FirstLvlComponent,
	TopMenuComponent,
	InformationComponent,
	AuthComponent
	
  ],
  imports: [
    CommonModule,
	FirstLvlRoutingModule,
	ClarityModule,
	HttpClientModule

  ]
})
export class FirstLvlModule { }
 