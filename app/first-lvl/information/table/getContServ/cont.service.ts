import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable, of} from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class ContService {

  constructor(private http: HttpClient) { }
  
  getTableContent(tableName: string) : Observable<any[][]>{
	return this.http.get<any[][]>('https://localhost:44321/api/'+tableName);
  }
  
   getTableOpt(tableName: string) : Observable<any[][]>{
	return this.http.get<any[][]>('https://localhost:44321/api/'+tableName + "/GetOpt");
  }
  
  getSortTableContent(tableName: string,  param) : Observable<string[][]>{
	return this.http.post<string[][]>('https://localhost:44321/api/'+tableName + "/PostSort", param);
  }
  
  addTableContent(tableName: string, content: string[]): Observable<number>{

	return this.http.post<number>('https://localhost:44321/api/'+tableName + "/Post", content); 
  }
  
  editTableContent(tableName: string, content: string[]): Observable<number>{

	return this.http.put<number>('https://localhost:44321/api/'+tableName, content); 
  }
  
  deleteTableContent(tableName: string, id: number): Observable<number>{

	return this.http.delete<number>('https://localhost:44321/api/'+tableName + '/' + id);
  }
}
