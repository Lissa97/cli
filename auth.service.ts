import { Injectable } from '@angular/core';
import { Observable, of} from 'rxjs';
import { HttpClient} from '@angular/common/http';

export class User{
	id: number;
	login: string;
	token: string;
	token2: string;
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }
  
   get(login: string, password: string) : Observable<User>{
	return this.http.get<User>('https://localhost:44321/api/Users/' + login + "/" + password);
   
   }
   
   getName(id: number) : Observable<string>{
	return this.http.post<string>('https://localhost:44321/api/Users/GetName',  {id: id, token: "45454"} );
   }
   
   
   
   getAccess(id: number, token: string) : Observable<boolean>{
	return this.http.post<boolean>('https://localhost:44321/api/Users/GetAccess',  {id: id, token: token} );
   }
   
   
   
    getAccessSecondToken(id: number, token: string) : Observable<User>{
	return this.http.post<User>('https://localhost:44321/api/Users/GetAccessSecondToken',  {id: id, token: token} );
   }
  
   
}
