import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable, of} from 'rxjs';

export class Menu{
	id: number;
	name: string;
	rus_name: string;
}

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private http: HttpClient) { }
  
  get() : Observable<Menu[]>{
	return this.http.get<Menu[]>('https://localhost:44321/api/T');
  }
  
  getTable(name: string) : Observable<Menu>{
	return this.http.get<Menu>('https://localhost:44321/api/T/'+name);
  }
}
