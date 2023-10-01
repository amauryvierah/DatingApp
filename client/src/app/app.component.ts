import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';//OnInit was added to the import


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {//implements OnInit to load httpclient call
  title = 'Dating App!';
  users: any;//Users variable declared

  constructor(private http: HttpClient) {}//Ctor added to use httpclient.

  ngOnInit(): void {//Interface implementation
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed')
    })
  }
}
