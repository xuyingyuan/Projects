import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {HttpClientModule} from '@angular/common/http';


import { AppComponent } from './app.component';


import { ProductModule } from './products/product.module';
import { AppRoutingModel } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent
  
  ],
  imports: [
    BrowserModule,
    HttpClientModule,    
    ProductModule,
    AppRoutingModel
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
