import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { Observable, of} from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthService} from '../auth.service';

@Component({
  selector: 'app-first-lvl',
  templateUrl: './first-lvl.component.html',
  styleUrls: ['./first-lvl.component.css']
})
export class FirstLvlComponent implements OnInit {
  name:string = "Скарлетт";
  blockName$:  Observable<string> ;
  blockName:string = "Finance";
  constructor(
		private serve: AuthService,
		private route: ActivatedRoute,
		private router: Router,
		private location: Location) { }

  ngOnInit() {
	  
	  this.serve.getName(parseInt(localStorage.getItem('id')))
	  .subscribe( x =>
			this.name = x
	  );
	  
	  this.getBlockName();
  }
  
  getBlockName(){
		
		this.blockName$ = this.route.paramMap.pipe(
			switchMap(params => {

			return of(params.get('blockName'));
			})
		  );
  
	}

}
