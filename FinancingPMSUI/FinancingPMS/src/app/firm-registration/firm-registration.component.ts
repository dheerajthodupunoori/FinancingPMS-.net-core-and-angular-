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
  firm = new Firm("", "", "", "");

  private registrationStatus: boolean = true;

  private firmRegistrationErrorMessage: string;

  constructor(
    private registerService: RegisterService,
    private router: Router
  ) {}

  ngOnInit() {}

  get diagnostic() {
    return JSON.stringify(this.firm);
  }

  RegisterFirm() {
    this.registerService.registerService(this.firm).subscribe({
      next: data => {
        console.error(data);
      },
      error: error => {
        console.error(
          "There was an error while registering your Firm.Please try again after some time",
          error
        );
        this.firmRegistrationErrorMessage =
          "There was an error while registering your Firm.Please try again after some time";
      }
    });

    if (this.registrationStatus == true) {
      this.router.navigate(["firm-additional-details", this.firm.Id]);
    }
  }
}
