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

  registerService(firm: Firm): Observable<Boolean> {
    console.log("Firm Details", firm);
    return this.http.post<Boolean>(this.registerURL, firm);
  }
}
