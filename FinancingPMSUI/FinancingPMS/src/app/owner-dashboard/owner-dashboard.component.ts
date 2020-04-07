import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-owner-dashboard",
  templateUrl: "./owner-dashboard.component.html",
  styleUrls: ["./owner-dashboard.component.css"],
})
export class OwnerDashboardComponent implements OnInit {
  public firmId: String = "";

  menuItems: any[];

  ROUTES: any[] = [
    { path: "/dashboard", title: "Dashboard", icon: "dashboard", class: "" },
    { path: "/user-profile", title: "User Profile", icon: "person", class: "" },
    {
      path: "/table-list",
      title: "Table List",
      icon: "content_paste",
      class: "",
    },
    {
      path: "/typography",
      title: "Typography",
      icon: "library_books",
      class: "",
    },
    { path: "/icons", title: "Icons", icon: "bubble_chart", class: "" },
    { path: "/maps", title: "Maps", icon: "location_on", class: "" },
    {
      path: "/notifications",
      title: "Notifications",
      icon: "notifications",
      class: "",
    },
    {
      path: "/upgrade",
      title: "Upgrade to PRO",
      icon: "unarchive",
      class: "active-pro",
    },
  ];

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    //getting firmID from route using ActivatedRoute
    this.firmId = this.route.snapshot.params["firmId"];
    this.menuItems = this.ROUTES.filter((menuItem) => menuItem);
  }
}
