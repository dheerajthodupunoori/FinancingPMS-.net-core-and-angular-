import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
    providedIn: "root"
  })
export class FirmService{

    private urlForFirmDetailsDropdown = "http://localhost:5000/api/Firm";

    constructor(private http: HttpClient) {}


    getAllFirms():Observable<any>{
        return this.http.get(this.urlForFirmDetailsDropdown)
    }

}