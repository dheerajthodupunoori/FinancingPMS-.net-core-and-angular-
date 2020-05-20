import { Component, OnInit } from '@angular/core';
import { RegisterCustomer } from '../Models/customer-register';

@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit {

 public details : RegisterCustomer = new RegisterCustomer("Dheeraj","","",new Date(),"","","","");

  constructor() { }

  ngOnInit() {
  }

}
