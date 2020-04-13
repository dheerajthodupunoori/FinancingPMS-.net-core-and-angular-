import { Component, OnInit } from "@angular/core";
import { LoginService } from "../Services/login.service";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.css"],
})
export class NavbarComponent implements OnInit {
  public loginStatus: boolean;

  constructor(private _loginService: LoginService) {}

  ngOnInit() {
    this._loginService.loginStatusSubject.subscribe((data) => {
      console.log("Login status subject in app component ", data);
      this.loginStatus = Boolean(data);
    });
  }

  Logout() {
    // this._loginService.updateLoginStatus("false");
    sessionStorage.clear();
  }
}
