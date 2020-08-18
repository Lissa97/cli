import { Component, OnInit, Inject, AfterViewInit,  ElementRef, ViewChild} from '@angular/core';
import {MenuService, Menu} from '../menu.service';
import {ContService} from './getContServ/cont.service';
import {RowService, Row} from './getContServ/row.service';
import { Observable, of} from 'rxjs';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { switchMap } from 'rxjs/operators';
import { DOCUMENT } from '@angular/common';
import {ClrDatagridStateInterface} from "@clr/angular";
import { HttpClient, HttpParams} from '@angular/common/http';


class P{
	sort: string;
	reverse: boolean;
	filter: string[];
}

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
	loading = true;

   users = [];
	
  rows : Row[] = [
	  {id: 1, table_id: 2, name: 'dd', rus_name: 's', type: 'd'}
  ];
  @ViewChild('modal_add', {static: false}) modal_add;  
  @ViewChild('modal_edit', {static: false}) modal_edit;
  
  selectData = [];
  editRow = [];
  
  selectTable: Menu = {'id': 1, 'name': 's', 'rus_name': 'nn'};
  tableName$: Observable<string>;
  tableName: string;

  editOpen : boolean = false;
  
  constructor(
  private http: MenuService,
  private httpCont: ContService,
  private httpRow: RowService,
  private route: ActivatedRoute,
  private router: Router,
  private location: Location,
  @Inject(DOCUMENT) document
  ) { }

  ngOnInit() {
	  this.getTableName();
	  
  }
  
  getTableName(){
		
		this.route.paramMap.pipe(
			switchMap(params => 

				of(params.get('tableName'))
				
			)
		  ).subscribe(name => {
				  this.tableName = name; 
				  this.ReloadContent();
			  });

	}
	ReloadContent(){
		

		this.http.getTable(this.tableName)
	    .subscribe(table => {
			
			this.selectTable = table;
			this.httpRow.getTableRow(this.selectTable.id)
				.subscribe(row =>{
					this.rows = row
					this.rows.forEach(row =>{
					if(row.type != 'string' && row.type != 'text' && row.type != 'int'){
						
						this.httpCont.getTableOpt(row.type)
						  .subscribe(cont => {
							  this.selectData[row.type] = cont;
							  
						  });
					}
					
				});			
				
				});
			
			});
		this.httpCont.getTableContent(this.tableName)
			 .subscribe(cont =>{  this.users = cont; this.loading = false; });
			 
			
	}
	
	onAdd(){
		//document.getElementById('modal_add').attributes['clrModalOpen'] = 'true';
	
		let content = [];
		let content2 = [];
		
		var i = 0;
		this.rows.forEach(row =>{
			var el = document.getElementById(row.name);
			
			if(el instanceof HTMLSelectElement){
				content2[row.name] = el.options[el.selectedIndex].text;
			    content[i] = el.value;
			}

			else if(el instanceof HTMLInputElement || el instanceof HTMLTextAreaElement){
				content2[row.name] = el.value;
			    content[i] = el.value;
			}
	
			i++;
		});
		//console.log(content);
		this.httpCont.addTableContent(this.selectTable.name, content)
		 .subscribe(id => {
			content2["id"] = id;
		    this.users[this.users.length] = content2;
		 });
		this.modal_add.close();
	}
	
	onEdit(){
		let content = [];
		let content2 = [];
		content[0] = this.editRow["id"].toString();
		content2["id"] = this.editRow["id"].toString();
		
		var i = 1;
		this.rows.forEach(row =>{
			var el = document.getElementById("edit_" + row.name);
			
			if(el instanceof HTMLSelectElement){
				content[i] = el.value;
				content2[row.name] = el.options[el.selectedIndex].text;
			}
				

			else if(el instanceof HTMLInputElement || el instanceof HTMLTextAreaElement){
				content2[row.name] = el.value;
				content[i] = el.value;
			}
			
			i++;
		});
		//console.log(content2);
		this.httpCont.editTableContent(this.selectTable.name, content)
		 .subscribe(id => {
			let index = this.users.findIndex(item => item["id"] == id.toString());
			this.users.splice(index, 1, content2);
		   
		 });
		this.modal_edit.close();
	}
	
	onDelete(el: string[]){
		this.httpCont.deleteTableContent(this.selectTable.name, parseInt(el["id"], 10))
		.subscribe(id => {
		  let index = this.users.findIndex(item => item["id"] == id);
		  this.users.splice(index, 1);
		});
	}
	
	onFilter(arr: string[][], item: string){
		if(arr === undefined||item==""){
			return arr;
		}
		
		let arrItem = arr.find(x => x[1] == item);
		let d = [arrItem].concat(arr.filter(x =>  x[1] != item));
		//console.log(d);
		return d;	
	}
	 
	onClickEdit(user){
		this.editRow = user; 
		this.modal_edit.open(); 
		
		this.rows.forEach( row=> {
			if(row.type != 'string' && row.type != 'text' && row.type != 'int'){

				this.selectData[row.type] = this.onFilter(this.selectData[row.type], this.editRow[row.name] );
				
				var g = 'edit_'+row.name
				setTimeout(function(){
				  
				  var el = document.getElementById(g);	
				  if(el instanceof HTMLSelectElement)
					  el.selectedIndex = 0;  
				},0);
								
			}
		})
				
	}
	
	refresh(state: ClrDatagridStateInterface) {

		var params = new P;

		let f = [];
		//state.filters = null;
		if (state.filters) {
			for(let i= 0; i < this.rows.length; i++){
				f[i] = "";
				for (let filter of state.filters) {
					if(filter.property == this.rows[i].name){
						f[i] = filter.value;
						
					}
				}
			}
        }
		
		else{
			for(let i= 0; i < this.rows.length; i++){
				f[i] = "";
			}
		}
		  
		params.filter = f;
		console.log(f);
		if(state.sort){
			params.sort = state.sort.by.toString();
			params.reverse = state.sort.reverse;
		}
		else{
			params.sort = "";
			params.reverse = false;
		}
			
			
		this.httpCont.getSortTableContent(this.tableName, params)
			 .subscribe(cont => {
				
				 this.users = cont;
				 
			 });
    }
	 arrayOne(n: number): any[] {
    return Array(n);
    }
}
