import { Component } from "@angular/core";
import { LoginService } from "./Services/login.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent {
  title = "FinancingPMS";

  public loginStatus: boolean;

  constructor(private _loginService: LoginService) {}
  ngOnInit() {
    // this.loginStatus = Boolean(localStorage.getItem("loginStatus"));
    // this._loginService.loginStatusSubject.subscribe((data) => {
    //   console.log("Login status subject in app component ", data);
    //   this.loginStatus = Boolean(data);
    // });
  }

  // Logout() {
  //   // this._loginService.updateLoginStatus("false");
  //   sessionStorage.clear();
  // }
}
