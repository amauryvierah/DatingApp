import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_model/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}

  
  constructor(public accountService: AccountService, private router: Router, 
    private toastr: ToastrService) {  }

  ngOnInit(): void {
    
  }

  login(){
    this.accountService.login(this.model).subscribe({
        next: _ => this.router.navigateByUrl('/members'),//Routing with code  _ or () would be the same
        error: error => this.toastr.error(error.error)
    })
  }

  logout(){
    this.accountService.logout();   
    this.router.navigateByUrl('/');//Routing with code 
  }
}
