import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
//import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule

import { AppComponent } from './app.component';
import { HelloWorldComponent } from './hello-world/hello-world.component';
import { provideHttpClient } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { PersonDetailsComponent } from './person-details/person-details.component';
import {PersonDataViewComponent} from './person-data-view/person-data-view.component';

@NgModule({
  declarations: [

  ],
  imports: [
    AppComponent,
    HelloWorldComponent,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    PersonDetailsComponent,
    PersonDataViewComponent,
    PersonDataViewComponent

  ],
  providers: [provideHttpClient()],
  bootstrap: []
})
export class AppModule { }
