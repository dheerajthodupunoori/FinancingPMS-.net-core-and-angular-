import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, Subject, BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class LoginService {


  private loginStatus  = new BehaviorSubject<boolean>
                            (Boolean(localStorage.getItem("loginStatus")) == null ? false :
                            Boolean(localStorage.getItem("loginStatus")));

  public loginStatusSubject = this.loginStatus.asObservable();


  private loginURL = "http://localhost:49366/api/Login";

  constructor(private http: HttpClient) {}

  login(loginData : any) :Observable<any> {
    
      const body = {
        FirmId : loginData.Id,
        Password : loginData.password
      }

      console.log("Body of the HttpPost login request " , body);

      console.log(loginData);
      return this.http.post<any>(this.loginURL,body
      );
  }


  updateLoginStatus(loginStatus : string)
  {
    localStorage.setItem("loginStatus" , loginStatus);
    this.loginStatus.next(Boolean(localStorage.getItem("loginStatus")));
  }


}
