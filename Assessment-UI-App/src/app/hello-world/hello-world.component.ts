import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';

import {NgIf} from '@angular/common';
import {NgFor} from '@angular/common';


@Component({
  selector: 'app-hello-world',
  standalone: true,
  imports: [NgIf,NgFor],
  templateUrl: './hello-world.component.html',
  styleUrl: './hello-world.component.css'
})
export class HelloWorldComponent implements OnInit{
// posts: any[];
 public message: string;
 public posts: Array<any>;

  constructor(private dataService: DataService) {
	//this.message = '';
	//this.posts = [];
  }

  ngOnInit(): void {
    // let greeting = `Hello from Service, ${name}!`;  // "Hello, Alice!"
    // return greeting;
  //   this.dataService.getMessage().subscribe(data => {
  //     this.message = `Hello from Service, ${data.message}!`;
  //   });
  //
  //
	// this.dataService.getMessage1().subscribe(data => {
  //     this.posts = data;
  //   });
  }
}
