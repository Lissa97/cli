import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent} from './app.component';
import { FirstLvlComponent } from './first-lvl/first-lvl.component';


const routes: Routes = [
	{ path: '', redirectTo: 'Information', pathMatch: 'full' },
	{ path: '', component: FirstLvlComponent, loadChildren:() => import('./first-lvl/first-lvl.module').then(m => m.FirstLvlModule) }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
