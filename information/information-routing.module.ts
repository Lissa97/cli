import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TableComponent } from './table/table.component';

const routes: Routes = [
	{ path: '', redirectTo: 'Groups', pathMatch: 'full' },
	{path: ':tableName', component: TableComponent}
	
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InformationRoutingModule { }
