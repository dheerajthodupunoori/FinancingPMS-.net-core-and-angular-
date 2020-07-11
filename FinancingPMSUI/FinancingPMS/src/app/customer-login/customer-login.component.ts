import { Component, OnInit } from '@angular/core';
import {FormGroup , FormControl , FormBuilder, Validators, AbstractControl} from '@angular/forms';
import { CustomerLogin } from '../Models/customer-login';

@Component({
  selector: 'app-customer-login',
  templateUrl: './customer-login.component.html',
  styleUrls: ['./customer-login.component.css']
})


export class CustomerLoginComponent implements OnInit {

  private validationMessages = {

    required : 'Password is required'

  };

  public passwordValidationMessage : string;

  public hasPasswordValidationError : boolean = false;

  customerLoginForm : FormGroup;

  customerLoginDetails = new CustomerLogin();

  constructor(private fb : FormBuilder) { }

  ngOnInit() {
    // this.customerLoginForm = new FormGroup({
    //   customerID : new FormControl(),
    //   password : new FormControl()
    // });

    this.customerLoginForm = this.fb.group({
      customerID : ['' , Validators.required], 
      password : ['' , Validators.required]
    });

    const passwordControl = this.customerLoginForm.get('password');
    passwordControl.valueChanges.subscribe(value => {
        this.setValidationMessage(passwordControl);
    });

  }

  LoginCustomer(){
    console.log("customer login details ", this.customerLoginForm.value);
  }

  public setValidationMessage(control : AbstractControl) : void {
      this.passwordValidationMessage = '';
this.hasPasswordValidationError = false;
      console.log(control.errors);

      if( (control.touched || control.dirty) && control.errors)
      {
      this.passwordValidationMessage = Object.keys(control.errors).map(
        key => this.validationMessages[key]).join(' ');
      this.hasPasswordValidationError = true;
      }
  }

}
