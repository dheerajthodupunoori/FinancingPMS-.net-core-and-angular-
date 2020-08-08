import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {requiredFileType} from '../validators/required-file-extension-validator'
import { RegisterService } from '../Services/register.service';

@Component({
  selector: 'app-customer-additional-details',
  templateUrl: './customer-additional-details.component.html',
  styleUrls: ['./customer-additional-details.component.css']
})
export class CustomerAdditionalDetailsComponent implements OnInit {

  public customerId : string = "";
  public detailsForm : FormGroup;
  public passport : File;
  public signature :File;

  constructor(private route: ActivatedRoute , private fb : FormBuilder , private _registrationService : RegisterService) { }

  ngOnInit() {
     //getting firmID from route using ActivatedRoute
     this.customerId = this.route.snapshot.params["customerId"];
    //  console.log("customer ID retrieved from route params in customer additional component is -->" ,this.customerId);

     this.detailsForm = this.fb.group({
      customerID : [this.customerId , Validators.required],
      occupation : ['Private job' , Validators.required],
      country : ['India' , Validators.required],
      state : ['Telangana' , Validators.required],
      city : ['Nizamabad' , Validators.required],
      zip : ['503001' , Validators.required],
      phone : ['7680052728' , Validators.required],
      flatNumber : ['10-14-1563/1' , Validators.required],
      street : ['Seetha Ram nagar colony' , Validators.required],
      income : ['60000' , Validators.required],
      signature : [null ,  [Validators.required , requiredFileType('png')]],
      passport : [null ,  [Validators.required , requiredFileType('png')]]
    });
    // console.log(this.detailsForm);
  }


  onChangeOfSignatureFile(event)
  {
    this.detailsForm.patchValue({signature : event.target.files[0]})
  }

  onChangeOfPassportPhoto(event : any)
  {
    console.log(event);
    console.log(this.detailsForm)
    this.detailsForm.patchValue({passport : event.target.files[0]})
  }

  SaveDetails()
  {
    console.log("Customer additional details", this.detailsForm);
    this._registrationService.saveCustomerAdditionalDetails(this.PrepareRequestData()).subscribe(response => {

    } , 
    error => {

    });
  }

  private PrepareRequestData()
  {
    const formData = new FormData();
    formData.append("signature" ,  this.detailsForm.value['signature']);
    formData.append("passport" ,  this.detailsForm.value['passport']);
      formData.append("customerID",this.detailsForm.value['customerID']);
      formData.append("occupation",this.detailsForm.value['occupation']);
      formData.append("country", this.detailsForm.value['country']);
      formData.append("state" ,this.detailsForm.value['state']);
      formData.append("city", this.detailsForm.value['city']);
      formData.append("zip" , this.detailsForm.value['zip']);
      formData.append("phone" , this.detailsForm.value['phone']);
      formData.append("flatNumber" , this.detailsForm.value['flatNumber']);
      formData.append("street" , this.detailsForm.value['street']);
      formData.append("income" , this.detailsForm.value['income']);
    // formData.append("additionalDetails",JSON.stringify(additionalDetails));
    return formData;
  }

}
