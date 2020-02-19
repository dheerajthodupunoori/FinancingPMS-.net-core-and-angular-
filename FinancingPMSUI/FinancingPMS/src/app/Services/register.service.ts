import { Injectable } from "@angular/core";
import { Firm } from "../Models/firm";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class RegisterService {
  private registerURL =
    "https://financingpms.azurewebsites.net/api/Registration";

  private registrationStatus: boolean = true;
  constructor(private http: HttpClient) {}

  registerService(firm: Firm) {
    console.log("Firm Details", firm);
    return this.http.post<Firm>(this.registerURL, firm);
  }
}
