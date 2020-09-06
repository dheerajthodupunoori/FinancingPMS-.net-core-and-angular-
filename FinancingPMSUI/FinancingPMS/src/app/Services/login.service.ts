import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, Subject, BehaviorSubject } from "rxjs";
import { LoginConfigLocal, LoginConfigServer } from "../../config/login-config";

// const customerLoginURL = "http://localhost:5000/api/Login/customerLogin";

@Injectable({
  providedIn: "root",
})
export class LoginService {
  private loginStatus = new BehaviorSubject<boolean>(
    Boolean(sessionStorage.getItem("loginStatus")) == null
      ? false
      : Boolean(sessionStorage.getItem("loginStatus"))
  );

  public loginStatusSubject = this.loginStatus.asObservable();

  // private loginURL = "http://localhost:5000/api/Login";

  constructor(private http: HttpClient) {}

  login(loginData: any): Observable<any> {
    const body = {
      FirmId: loginData.Id,
      Password: loginData.password,
    };

    console.log("Body of the HttpPost login request ", body);

    console.log(loginData);
    return this.http.post<any>(
      LoginConfigServer.firmloginURL === ""
        ? LoginConfigLocal.firmloginURL
        : LoginConfigServer.firmloginURL,
      body
    );
  }

  updateLoginStatus(loginStatus: string) {
    sessionStorage.setItem("loginStatus", loginStatus);
    this.loginStatus.next(Boolean(sessionStorage.getItem("loginStatus")));
  }

  loginCustomer(customerLogin: any): Observable<any> {
    return this.http.post<any>(
      LoginConfigServer.customerLoginURL === ""
        ? LoginConfigLocal.customerLoginURL
        : LoginConfigLocal.customerLoginURL,
      customerLogin
    );
  }
}
