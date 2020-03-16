import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { FormsModule } from "@angular/forms";
import { FooterComponent } from "./footer/footer.component";
import { FirmRegistrationComponent } from "./firm-registration/firm-registration.component";
import { LandingPageComponent } from "./landing-page/landing-page.component";
import { HttpClientModule } from "@angular/common/http";
import { FirmAdditionalDetailsComponent } from "./firm-additional-details/firm-additional-details.component";
import { LoginComponent } from './login/login.component';
import { CustomerLoginComponent } from './customer-login/customer-login.component';
import { OwnerDashboardComponent } from './owner-dashboard/owner-dashboard.component';

const appRoutes: Routes = [
  { path: "", component: LandingPageComponent },
  { path: "register-firm", component: FirmRegistrationComponent },
  {
    path: "firm-additional-details/:firmId",
    component: FirmAdditionalDetailsComponent
  },
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "customer-login",
    component: CustomerLoginComponent
  },
  {
    path: "owner-dashboard",
    component: OwnerDashboardComponent
  }
];

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    FirmRegistrationComponent,
    LandingPageComponent,
    FirmAdditionalDetailsComponent,
    LoginComponent,
    CustomerLoginComponent,
    OwnerDashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes
      // <-- debugging purposes only
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
