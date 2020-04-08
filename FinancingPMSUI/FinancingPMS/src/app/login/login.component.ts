import { Component, OnInit } from "@angular/core";
import { LoginService } from "../Services/login.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  public loginErrorMessage: string = "";

  constructor(private _loginService: LoginService, private router: Router) {}

  ngOnInit() {}

  LoginFirm(loginData) {
    this._loginService.login(loginData).subscribe(
      (response) => {
        console.log(response);
        // localStorage.setItem("jwt", response.jsonToken);
        sessionStorage.setItem("jwt", response.jsonToken);
        //  localStorage.setItem("loginStatus" , response.loginStatus);
        this._loginService.updateLoginStatus(response.loginStatus);
        if (
          response.loginStatus == true &&
          response.areFirmDetailsSaved == false
        ) {
          this.router.navigate(["firm-additional-details", loginData.Id]);
        } else {
          if (response.loginStatus == true) {
            this.router.navigate(["owner-dashboard", loginData.Id]);
          }
        }

        if (response.errorMessage) {
          this.loginErrorMessage = response.errorMessage;
        }
      },
      (error) => {
        console.log(error);
        this.loginErrorMessage = error.message;
      }
    );
  }
}
