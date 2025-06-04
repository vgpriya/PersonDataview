import {ChangeDetectorRef, Component} from '@angular/core';
import {DataService} from '../data.service';
import {NgFor, NgIf} from '@angular/common';
import {Person} from '../model/person-data-model';
import {Router, RouterLink} from '@angular/router';
@Component({
  selector: 'app-person-details',
  standalone: true,
  imports: [NgIf, NgFor, RouterLink],
  templateUrl: './person-details.component.html',
  styleUrl: './person-details.component.css'
})
export class PersonDetailsComponent {
  public message: string;
  public posts: Array<any>;
  public personArray: Array<Person>;
  public person: Person;


  constructor(private dataService: DataService, private router: Router, private cdr: ChangeDetectorRef) {

  }

  onClickSend(id:string) {
    this.router.navigate(['/person-data-view', id]);
  }
  onPersonClick(personId: string): void {
    this.dataService.getPersonData(personId).subscribe(data => {
      this.person = data;
    });
  }
  ngOnInit(): void {

    this.dataService.getPersonDetails().subscribe(data => {
      this.personArray = data;
      this.cdr.detectChanges();
    });

  }
}
