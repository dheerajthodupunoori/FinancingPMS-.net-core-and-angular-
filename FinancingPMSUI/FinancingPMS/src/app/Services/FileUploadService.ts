import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
    providedIn: "root"
  })
export class FileUploadOperationsService{

    private aadhaarUploadForCustomerRegistrationURL = 
    "http://localhost:5000/api/AzureBlobOperations​/UploadAadhaarImageToAzureBlobContainer";

    private imageToTextFunctionURL = "https://imagecontenttotext.azurewebsites.net/api/ExtractTextFromImage"; 
    
    private getTextFromImageURL = "http://localhost:5000/api/CustomerRegistration/GetTextFromImage";
    


    constructor(private http: HttpClient) {}


    uploadAadhaarImage(aadhaarImage : FormData , customerID:string):Observable<any>{
        
        return this.http.post<any>("http://localhost:5000/api/AzureBlobOperations/UploadAadhaarImageToAzureBlobContainer", aadhaarImage,
        {
          reportProgress:true,
          params: new HttpParams().set('customerID', customerID)
          }
);

    }


    retrieveTextFromImage(aadhaarImage : FormData):Observable<any>{

      return this.http.post<any>(this.getTextFromImageURL , aadhaarImage);

    }
     
}