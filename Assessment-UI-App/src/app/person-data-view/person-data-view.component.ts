import {Component, Input, OnInit,NgZone,ChangeDetectorRef } from '@angular/core';
import {DataService} from '../data.service';
import {RouterLink} from '@angular/router';
import {Person} from '../model/person-data-model';
import {AsyncPipe, JsonPipe, NgIf} from '@angular/common';
import {filter, switchMap} from 'rxjs';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-person-data-view',
  standalone: true,
  imports: [
    RouterLink,
    NgIf,
    AsyncPipe,
    JsonPipe
  ],
  templateUrl: './person-data-view.component.html',
  styleUrl: './person-data-view.component.css'
})
export class PersonDataViewComponent implements OnInit {
  public person: Person;
  public id: string;


  constructor(private dataService: DataService,private route: ActivatedRoute) {
  }

  ngOnInit() {
    const personId = this.route.snapshot.paramMap.get('id');
    if (personId) {
      this.dataService.getPersonData(personId).subscribe({
        next: data => {
          this.person = data;
        },        error: err => {
          console.error('Error fetching person data:', err);
        }
      });
    }
  }
}
