import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-firm-details",
  templateUrl: "./firm-details.component.html",
  styleUrls: ["./firm-details.component.css"]
})
export class FirmDetailsComponent implements OnInit {
  private firmId;

  constructor(private _activatedroute: ActivatedRoute) {}

  ngOnInit() {
    // this.firmId = this._activatedroute.snapshot.paramMap.get("firmId");
    // this._activatedroute.paramMap.subscribe(params => {
    //   this.firmId = params.get("firmId");
    // });

    this.firmId = this._activatedroute.snapshot.params.firmId;
    console.log(this.firmId);
  }
}
