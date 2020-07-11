import { Component, OnInit } from "@angular/core";
import { Firm } from "../Models/firm";
import { RegisterService } from "../Services/register.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-firm-registration",
  templateUrl: "./firm-registration.component.html",
  styleUrls: ["./firm-registration.component.css"]
})
export class FirmRegistrationComponent implements OnInit {
  public firm :Firm = new Firm("", "" , "", "" , "", "");

  public registrationStatus: boolean;

  public firmRegistrationErrorMessage: string;

  public response :any;

  constructor(
    private registerService: RegisterService,
    private router: Router
  ) {}

  ngOnInit() {}

  get diagnostic() {
    return JSON.stringify(this.firm);
  }

//   isNumber(evt) {
//     console.log(evt);
//     evt = (evt) ? evt : window.event;
//     var charCode = (evt.which) ? evt.which : evt.keyCode;
//     if (charCode > 31 && (charCode < 48 || charCode > 57)) {
//       event.preventDefault();
//     }
// }

  async RegisterFirm() {
    await this.registerService.registerService(this.firm).subscribe(
      data => {
        console.log("Firm registration response " , data);
        this.registrationStatus = data.registrationStatus;
        console.log("Firm registration status" , this.registrationStatus);
        if (this.registrationStatus == true) {
          this.router.navigate(["login"]);
        }
        else
        {
          this.firmRegistrationErrorMessage = data.errorDetails[0];
        }
      }
     );
  }
}
