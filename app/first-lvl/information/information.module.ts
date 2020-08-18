
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InformationRoutingModule} from './information-routing.module';

import { ClarityModule } from '@clr/angular';
import { TableComponent } from './table/table.component';
import { HttpClientModule }   from '@angular/common/http';

@NgModule({
  declarations: [

  TableComponent
  ],
  imports: [
    CommonModule,
	ClarityModule,
	InformationRoutingModule,
	HttpClientModule
	
  ],
  providers: [],
  bootstrap: []
})
export class InformationModule { }
