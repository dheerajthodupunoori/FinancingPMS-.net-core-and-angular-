import { Component, OnInit } from '@angular/core';
import {FormGroup , FormControl , FormBuilder, Validators, AbstractControl} from '@angular/forms';
import { CustomerLogin } from '../Models/customer-login';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';

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

  public hasLoginError : boolean = false;

  public loginErrorMessage : string;

  constructor(private fb : FormBuilder , private _loginService : LoginService,private router: Router) { }

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
        console.log(value);
        this.setValidationMessage(passwordControl);
    console.log(this.customerLoginForm);

    });
    console.log(this.customerLoginForm);
  }

  LoginCustomer(){
    console.log("customer login details ", this.customerLoginForm.value.customerID);
    // this.customerLoginForm.patchValue({password :['asdffg']});
    // console.log("customer login details after login", this.customerLoginForm.value);

    this._loginService.loginCustomer(this.customerLoginForm.value).subscribe(data => {
        console.log(data);
        if(data.customerLoginStatus)
        {
          this.hasLoginError = false;
          if(data.areCustomerAdditionalDetailsSaved)
          {

          }
          else
          {
            this.router.navigate(["customer-additional-details", this.customerLoginForm.value.customerID]);
          }
        }
        else
        {
          this.hasLoginError = true;
          this.loginErrorMessage = data.errorMessage;
        }
    },
    error => {
        console.log(error);
        this.hasLoginError = true;
        this.loginErrorMessage = error.message;
    });

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
