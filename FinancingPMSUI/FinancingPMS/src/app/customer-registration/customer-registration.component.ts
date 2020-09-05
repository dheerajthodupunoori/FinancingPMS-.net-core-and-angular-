import { Component, OnInit } from '@angular/core';
import { RegisterCustomer } from '../Models/customer-register';
import {FirmService} from '../Services/FirmService';
import {CustomerRegistrationStatusEnum} from '../Enums/CustomerRegistrationValidationStatusEnum';
import { RegisterService } from '../Services/register.service';
import {FileUploadOperationsService} from '../Services/FileUploadService';
import { HttpEventType } from '@angular/common/http';
import { Router } from '@angular/router';
// import {Tesseract} from 'tesseract.js'


@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit {

  //initial details for formindex
 public details : RegisterCustomer = new RegisterCustomer("Dheeraj","Thodupunuri","Dilip Raju",null,"","","","dheeraj.thodupunoori01@gmail.com");

 public firmDropdownListItems : any[];

 public isDropdownError:boolean = false;

 public dropdownErrorMessage :string ;

 public fileToUpload : any;

 public hasRegistrationError:boolean=false;

 public registrationErrorMessage:string;

 public CustomerID:string;

 public hasAadhaarFileUploadError : boolean = false;

 public aadhaarFileUploadErrorMessage : string;


  constructor(private _firmService:FirmService,
            private _registerService:RegisterService,
            private _fileUploadService:FileUploadOperationsService,
            private router: Router) { }

  ngOnInit() {
    this._firmService.getAllFirms().subscribe((result)=>{
      console.log("all firm details in customer-registration-component" , result);
      this.firmDropdownListItems = result;
      this.details.FirmID=result[0].id;
      console.log("firmDropdownListItems" , this.firmDropdownListItems);
      // console.log("registration details of customer" ,  this.details);
    },
    (error)=>
    {
      console.log(error);
      this.isDropdownError=true;
      this.dropdownErrorMessage = error.message;
    });
  }


  handleFileInput(files: any) {  
    console.log("uploaded files", files);
    let formData: FormData = new FormData();  
    for (let file of files){
    formData.append(file.name, file);
    }
    this.fileToUpload = formData;

    // var Tesseract = window.Tesseract;
    // Tesseract.recognize(files[0]).then(function(result){
    //   console.log(result.text);
    //   });

    // this._fileUploadService.retrieveTextFromImage(this.fileToUpload).subscribe((data) => {
    //     console.log(data);
    // });

    // const worker = new Tesseract.TesseractWorker();
    // worker.recognize(this.fileToUpload)
    // .progress(function(packet){
    //     console.info(packet)
    // })
    // .then(function(data){
    //     console.log(data)
    // })
    
    console.log("file to upload" , this.fileToUpload);
  } 


  RegisterCustomer()
  {
    this.hasRegistrationError=false;
    console.log("customer registration details in component class" , this.details);
    this.details.CustomerRegistrationValidationStatus = CustomerRegistrationStatusEnum.NotValidated;
    this._registerService.registerCustomerToFirm(this.details).subscribe((data)=>{
      console.log(data);
      this.CustomerID = data.customerID;
      console.log("Customer ID" , this.CustomerID);
      this.uploadAadhaarImage(this.fileToUpload,this.CustomerID);
    },
    (error)=>{
      console.log("error" , error);
      this.hasRegistrationError = true;
      this.registrationErrorMessage = error.error;
    });
  }

  uploadAadhaarImage(fileToUpload : any , customerID:string){

    this._fileUploadService.uploadAadhaarImage(fileToUpload,customerID).subscribe((data)=>
    {
      console.log("success response from azure aadhaar image upload service" , data);
      if(data){
        this.router.navigate(["customer-login"]);
      }
    },
    (error)=>
    {
      console.log("Error response from azure aadhaar image upload service" ,error);
      this.hasAadhaarFileUploadError=true;
      this.aadhaarFileUploadErrorMessage = error.message;
    });
    
  }





}
