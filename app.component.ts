import { Component, OnInit } from '@angular/core';
import { ClrCommonStrings } from '@clr/angular';
import { ClrCommonStringsService } from '@clr/angular';

export const klingonLocale: ClrCommonStrings = {
  open: 'ghIt',
  close: 'SoQmoH'
}
  
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent{
	

	title = 'cl';
	
	constructor(commonStrings: ClrCommonStringsService) {
		// Call this method to set the new locale values into the service, defaults for English
		// will be used for in any missing strings
		commonStrings.localize(klingonLocale);
	  }
}



