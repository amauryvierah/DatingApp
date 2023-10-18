import { Component, OnInit } from '@angular/core';//OnInit was added to the import
import { AccountService } from './_services/account.service';
import { User } from './_model/user';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {//implements OnInit to load httpclient call
  title = 'Dating App!';


  constructor(private accountService: AccountService) {}//Ctor added to use httpclient.

  ngOnInit(): void {//Interface implementation
    this.setCurrentUser();
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
