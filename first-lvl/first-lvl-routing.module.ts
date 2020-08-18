import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InformationComponent } from './information/information.component';

import { AuthComponent } from './auth/auth.component';
import { AuthGuard}  from '../auth.guard';

const routes: Routes = [
	{ path: '', redirectTo: 'Information', pathMatch: 'full' },
	{path: 'Information', component: InformationComponent, loadChildren:() => import('./information/information.module').then(m => m.InformationModule) , canActivate: [AuthGuard]},
	{path: 'Auth', component: AuthComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FirstLvlRoutingModule { }
