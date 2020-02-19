import { Component, OnInit } from "@angular/core";
import { Firm } from "../Models/firm";
import { RegisterService } from "../Services/register.service";

@Component({
  selector: "app-firm-registration",
  templateUrl: "./firm-registration.component.html",
  styleUrls: ["./firm-registration.component.css"]
})
export class FirmRegistrationComponent implements OnInit {
  firm = new Firm("", "", "", "");

  private registrationStatus: boolean = true;

  constructor(private registerService: RegisterService) {}

  ngOnInit() {}

  get diagnostic() {
    return JSON.stringify(this.firm);
  }

  RegisterFirm() {
    this.registerService.registerService(this.firm);
  }
}
