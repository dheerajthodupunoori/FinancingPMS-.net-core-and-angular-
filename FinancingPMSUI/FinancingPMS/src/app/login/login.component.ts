import { Component, OnInit } from '@angular/core';
import { LoginService } from "../Services/login.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginErrorMessage : string = "";

  constructor(private _loginService : LoginService, private router: Router) { }

  ngOnInit() {
  }

  LoginFirm(loginData)
  {
    //  console.log("Component logs " , loginData);

    this._loginService.login(loginData).subscribe(
      (response) =>{
       console.log(response);
       localStorage.setItem("jwt", response.jsonToken);
      //  localStorage.setItem("loginStatus" , response.loginStatus);
          this._loginService.updateLoginStatus(response.loginStatus);
        if(response.loginStatus == true)
        {
          this.router.navigate(["firm-additional-details" , loginData.Id]);
        }

        else
        {
          this.loginErrorMessage  = response.ErrorMessage;
        }


      },
      (error) => {
        console.log(error);
      }
    );

  }

}
