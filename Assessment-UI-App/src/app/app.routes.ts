import { Routes } from '@angular/router';
import {HelloWorldComponent} from './hello-world/hello-world.component';
import {PersonDetailsComponent} from './person-details/person-details.component';
import {PersonDataViewComponent} from './person-data-view/person-data-view.component';

export const routes: Routes = [
  { path: 'person-details', component: PersonDetailsComponent },
  { path: 'person-data-view/:id', component: PersonDataViewComponent },
  { path: '', redirectTo: '/person-details', pathMatch: 'full' }

];
