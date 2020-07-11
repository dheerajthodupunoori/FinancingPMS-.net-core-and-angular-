import { Injectable } from "@angular/core";
import { Firm } from "../Models/firm";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { FirmDetails } from "../Models/firm-details";
import { RegisterCustomer } from '../Models/customer-register';

@Injectable({
  providedIn: "root"
})
export class RegisterService {
  private registerURL = "http://localhost:5000/api/Registration";

  private saveFirmDetailsURL =
    "http://localhost:49366/api/Registration/saveFirmDetails";

  private registerCustomerToFirmURL = "http://localhost:5000/api/CustomerRegistration/PerformCustomerRegistration";

  constructor(private http: HttpClient) {}

  registerService(firm: Firm): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json; charset=utf-8"
      })
    };
    const body = {
      name: firm.Name,
      id: firm.Id,
      email: firm.Email,
      phoneNumber: firm.PhoneNumber,
      password: firm.password
    };
    console.log("Firm Details", body);

    return this.http.post<any>(this.registerURL, body, httpOptions);
  }

  saveFirmDetails(firmDetails: FirmDetails) {
    console.log("Firm Details entered by user", firmDetails);

    // const httpOptions = {
    //   headers: new HttpHeaders({
    //     "Content-Type": "application/json; charset=utf-8"
    //   })
    // };

    // const body = {
    //   Address1: firmDetails.Address1,
    //   Address2: firmDetails.Address2,
    //   City: firmDetails.City,
    //   State: firmDetails.State,
    //   Zip: firmDetails.Zip,
    //   FirmId: firmDetails.FirmId
    // };

    return this.http.post<any>(this.saveFirmDetailsURL, firmDetails);
  }


  registerCustomerToFirm(customerLoginDetails:RegisterCustomer) : Observable<any>{
    console.log("customer registration details in register service " , customerLoginDetails);
          return this.http.post<any>(this.registerCustomerToFirmURL,customerLoginDetails,
                        {
                          reportProgress:true
                        });
  }



}
