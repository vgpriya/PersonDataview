import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,Subject } from 'rxjs';
import {Person} from './model/person-data-model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  // private messageSource = new Subject<string>();
  // message$ = this.messageSource.asObservable();
  // private personSource = new Subject<Person>();
  // person$ = this.personSource.asObservable();

  private sharedID: any;
  constructor(private http: HttpClient) {
        this.http = http;

    }
  getPersonDetails(): Observable<Array<Person>> {

    return this.http.get<Array<Person>>('https://localhost:7034/Person/list');
  }
  getPersonData(personId: string): Observable<Person> {

    return this.http.get<Person>(`https://localhost:7034/Person/${personId}`);
  }
}
