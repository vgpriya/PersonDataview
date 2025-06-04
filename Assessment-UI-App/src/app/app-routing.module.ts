import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HelloWorldComponent } from './hello-world/hello-world.component';
import { PersonDetailsComponent } from './person-details/person-details.component';
import {PersonDataViewComponent} from './person-data-view/person-data-view.component';

const routes: Routes = [
  //{ path: 'hello', component: HelloWorldComponent },
   { path: 'person-details', component: PersonDetailsComponent },
  // {
  //   path: 'person-data-view',
  //   loadComponent: () =>
  //     import('./person-data-view/person-data-view.component').then(m => m.PersonDataViewComponent),
  // },
  { path: 'person-data-view/:id', component: PersonDataViewComponent },
  //{ path: '', redirectTo: '/persondetails', pathMatch: 'full' }

];



@NgModule({
  declarations: [],
  imports: [
    CommonModule,RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
