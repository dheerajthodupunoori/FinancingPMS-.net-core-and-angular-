import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { FileUploadConfigLocal } from "../../config/file-upload-config";
import { FileUploadConfigServer } from "../../config/file-upload-config";
import { ConfigUtility } from "../utilities/config-utility";
import { FileUpload } from "../Enums/OperationType";

@Injectable({
  providedIn: "root",
})
export class FileUploadOperationsService {
  // private aadhaarUploadForCustomerRegistrationURL =
  // "http://localhost:5000/api/AzureBlobOperations​/UploadAadhaarImageToAzureBlobContainer";

  // private getTextFromImageURL = "http://localhost:5000/api/CustomerRegistration/GetTextFromImage";

  constructor(private http: HttpClient) {}

  uploadAadhaarImage(
    aadhaarImage: FormData,
    customerID: string
  ): Observable<any> {
    return this.http.post<any>(
      ConfigUtility.GetURL(FileUpload.AadhaarUpload),
      aadhaarImage,
      {
        reportProgress: true,
        params: new HttpParams().set("customerID", customerID),
      }
    );
  }

  // retrieveTextFromImage(aadhaarImage : FormData):Observable<any>{

  //   return this.http.post<any>(this.getTextFromImageURL , aadhaarImage);

  // }
}
