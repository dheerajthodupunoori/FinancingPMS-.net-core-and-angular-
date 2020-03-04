import { Component, OnInit } from '@angular/core';
import { LoginService } from "../Services/login.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private _loginService : LoginService) { }

  ngOnInit() {
  }

  LoginFirm(loginData)
  {
    // console.log(loginData);

    this._loginService.login(loginData.Id);

  }

}
