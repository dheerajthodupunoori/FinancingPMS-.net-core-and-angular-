import { Component, OnInit } from "@angular/core";
import { Firm } from "../Models/firm";

@Component({
  selector: "app-firm-registration",
  templateUrl: "./firm-registration.component.html",
  styleUrls: ["./firm-registration.component.css"]
})
export class FirmRegistrationComponent implements OnInit {
  firm = new Firm("", "", "", "");

  constructor() {}

  ngOnInit() {}

  get diagnostic() {
    return JSON.stringify(this.firm);
  }

  RegisterFirm() {}
}
