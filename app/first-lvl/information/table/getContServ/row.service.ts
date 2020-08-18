import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable, of} from 'rxjs';

export class Row{
	id: number;
	table_id: number;
	name: string;
	rus_name: string;
	type: string;
}


@Injectable({
  providedIn: 'root'
})
export class RowService {

  constructor(private http: HttpClient) { }
  
  getTableRow(tableId: number) : Observable<Row[]>{
	return this.http.get<Row[]>('https://localhost:44321/api/RowTypes/'+tableId);
  }
}
