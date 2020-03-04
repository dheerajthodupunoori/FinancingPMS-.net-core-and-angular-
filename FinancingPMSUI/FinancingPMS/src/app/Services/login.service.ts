import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class LoginService {
  private loginURL = "http://localhost:49366/api/Login";

  constructor(private http: HttpClient) {}

  login(Id : string) :Observable<any> {
    
    const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8'
        })
      };

      const body = {
          id : Id
      }

      console.log(Id);
      return this.http.post<any>(this.loginURL,body
      );
  }


}
