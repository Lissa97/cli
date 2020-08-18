import { Component, OnInit, Inject, AfterViewInit,  ElementRef, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import { DOCUMENT } from '@angular/common';
import {AuthService, User} from '../../auth.service';
import {catchError } from 'rxjs/operators';
import { Observable, of} from 'rxjs';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
	error: boolean = false;
	
  constructor(
	private  httpAuth: AuthService, 
	private router: Router,
	@Inject(DOCUMENT) document
  ) { }

  ngOnInit() {
	  localStorage.setItem('auth_token', "0");
	  localStorage.setItem('auth_token2', "0");
  }
  
  tryLogIn(){
	  let resolt = new User();
	  
	  var login = document.getElementById('username') ;
	  var password = document.getElementById('password');
		if(login instanceof HTMLInputElement){
			if( password instanceof HTMLInputElement){
				
				
			  this.httpAuth.get(login.value, password.value)
			    
				.subscribe(res =>
				{
					if(typeof(res) != undefined){
						resolt = res;
						localStorage.setItem('auth_token', resolt.token);
						localStorage.setItem('auth_token2', resolt.token2);
						localStorage.setItem('id', resolt.id.toString());
						this.router.navigate(['/Information']);
					}

				}
				, err => {
   
					  if (err.status === 404) {
						this.error = true;
					  }
					 
					}
				)
			    ;
			}
		}  
		   
		
		
	
  }

}
