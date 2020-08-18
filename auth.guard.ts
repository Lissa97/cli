import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, CanLoad, Route, Router, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import {AuthService, User} from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {
	 constructor(
	private  httpAuth: AuthService, 
	private router: Router,
  ) { }
  
  async canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<boolean | UrlTree> {
	
		
	var token = localStorage.getItem('auth_token');	
	var id = parseInt(localStorage.getItem('id'));
	
	if(token == null || id == null){
		this.router.navigate(['/Auth']);
		
	}
	
	var res = false;
	
	await new Promise((resolve, reject) => {
        this.httpAuth.getAccess(id, token)
	    .subscribe(x => {
			res = x; 
			resolve(x);
		} );
		
    });
	
    if(res){
		
		return  Promise.resolve(res);
	}
		
	
	else{
	

		var user = new User();
		var err = false;
		await new Promise((resolve, reject) => {
			this.httpAuth.getAccessSecondToken(id, localStorage.getItem('auth_token2'))
			.subscribe(
			x => {
				user = x; 
				resolve(x);
			},
			err => { 
			  err = true;
			 this.router.navigate(['/Auth']);
			
			  
			}
			);
        });
		
		if(!err)
		{
			localStorage.setItem('auth_token', user.token);	
			localStorage.setItem('auth_token2', user.token2);	
		}
		console.log(err);
		return Promise.resolve(!err);
	}
  }
  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }
  canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return true;
  }
}
