import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {FirmConfigLocal} from '../../config/firm-config';
import {FirmConfigServer} from '../../config/firm-config';


@Injectable({
    providedIn: "root"
  })
export class FirmService{

    // private urlForFirmDetailsDropdown = "http://localhost:5000/api/Firm";

    constructor(private http: HttpClient) {}

    getAllFirms():Observable<any>{
        return this.http.get(FirmConfigServer.getAllFirms === "" ?  FirmConfigLocal.getAllFirms : FirmConfigServer.getAllFirms)
    }

}