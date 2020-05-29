import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-customer-additional-details',
  templateUrl: './customer-additional-details.component.html',
  styleUrls: ['./customer-additional-details.component.css']
})
export class CustomerAdditionalDetailsComponent implements OnInit {

  public customerId : string = "";

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
     //getting firmID from route using ActivatedRoute
     this.customerId = this.route.snapshot.params["customerId"];
     console.log("customer ID retrieved from route params in customer additional component is -->" ,this.customerId)
  }

}
