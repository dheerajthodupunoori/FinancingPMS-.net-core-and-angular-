import { Component, OnInit } from '@angular/core';
import { RegisterCustomer } from '../Models/customer-register';
import {FirmService} from '../Services/FirmService';

@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit {

  //initial details for form
 public details : RegisterCustomer = new RegisterCustomer("","","",new Date(),"","","","");

 public firmDropdownListItems : any[];

 public isDropdownError:boolean = false;

 public dropdownErrorMessage :string ;

 public fileToUpload : FormData;
 

  constructor(private _firmService:FirmService) { }

  ngOnInit() {
    this._firmService.getAllFirms().subscribe((result)=>{
      console.log("all firm details in customer-registration-component" , result);
      this.firmDropdownListItems = result;
      this.details.FirmID=result[0].id;
      console.log("firmDropdownListItems" , this.firmDropdownListItems);
      console.log("registration details of customer" ,  this.details);
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
    formData.append("asset", files[0], files[0].name);  
    this.fileToUpload = formData;  
    console.log("file to upload" , formData);
  } 


  RegisterCustomer()
  {
    
  }





}
