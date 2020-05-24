import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
    providedIn: "root"
  })
export class FileUploadOperationsService{

    private aadhaarUploadForCustomerRegistrationURL = 
    "http://localhost:5000/api​/AzureBlobOperations​/UploadAadhaarImageToAzureBlobContainer";

    constructor(private http: HttpClient) {}


    uploadAadhaarImage(aadhaarImage : FormData , customerID:string):Observable<any>{

        // const body = {
        //     aadhaarImage : aadhaarImage,
        //     customerID : customerID
        // };
        
        
        return this.http.post<any>(this.aadhaarUploadForCustomerRegistrationURL,
                                            aadhaarImage,
                                            {
                                            reportProgress:true,
                                            headers: {
                                                'Content-Type': 'multipart/form-data'
                                              }
                                        //  params: new HttpParams().set('customerID', customerID)
                                            });

    }
     
}