import { Component, OnInit } from "@angular/core";
import { FirmDetails } from "../Models/firm-details";
import { ActivatedRoute, Router } from "@angular/router";
import { RegisterService } from "../Services/register.service";

@Component({
  selector: "app-firm-additional-details",
  templateUrl: "./firm-additional-details.component.html",
  styleUrls: ["./firm-additional-details.component.css"]
})
export class FirmAdditionalDetailsComponent implements OnInit {
  public firmDetails: FirmDetails = new FirmDetails("", "", "", "", "", "");

  public errorMessage: string = "";

  constructor(
    private route: ActivatedRoute,
    private _registerService: RegisterService,
    private router: Router
  ) {}

  ngOnInit() {
    //getting firmID from route using ActivatedRoute
    this.firmDetails.FirmId = this.route.snapshot.params["firmId"];
  }

  SaveFirmDetails() {
    this._registerService.saveFirmDetails(this.firmDetails).subscribe(
      data => {
        console.log(data);
        this.router.navigate(["owner-dashboard", this.firmDetails.FirmId]);
      },
      error => {
        console.log(error);
        this.errorMessage = error.error.title;
      }
    );
  }
}
