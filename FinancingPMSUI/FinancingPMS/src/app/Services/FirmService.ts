import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ConfigUtility } from "../utilities/config-utility";
import { Firm } from "../Enums/OperationType";

@Injectable({
  providedIn: "root",
})
export class FirmService {
  // private urlForFirmDetailsDropdown = "http://localhost:5000/api/Firm";

  constructor(private http: HttpClient) {}

  getAllFirms(): Observable<any> {
    return this.http.get(ConfigUtility.GetURL(Firm.GetAllFirms));
  }
}
