import { Injectable } from "@angular/core";
import { Firm } from "../Models/firm";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class RegisterService {
  private registerURL = "http://localhost:49366/api/Registration";

  constructor(private http: HttpClient) {}

  registerService(firm: Firm): Observable<any> {
    

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      })
    };

    const body ={
      name : firm.Name,
      id:firm.Id,
      email:firm.Email,
      phoneNumber :firm.PhoneNumber
    };

    
     console.log("Firm Details", body);

    

    return this.http.post<any>(this.registerURL,body ,httpOptions
    );
  }
}
