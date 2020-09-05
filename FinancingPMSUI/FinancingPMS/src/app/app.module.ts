import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { FormsModule , ReactiveFormsModule } from "@angular/forms";
import { FooterComponent } from "./footer/footer.component";
import { FirmRegistrationComponent } from "./firm-registration/firm-registration.component";
import { LandingPageComponent } from "./landing-page/landing-page.component";
import { HttpClientModule } from "@angular/common/http";
import { FirmAdditionalDetailsComponent } from "./firm-additional-details/firm-additional-details.component";
import { LoginComponent } from "./login/login.component";
import { CustomerLoginComponent } from "./customer-login/customer-login.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatMenuModule } from "@angular/material/menu";
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from "./auth-guards/authguard";
import { NavbarComponent } from './navbar/navbar.component';
import { CustomerRegistrationComponent } from './customer-registration/customer-registration.component';
import {ConfirmPassword} from './validators/password-confirmpwd-validator';
import {AadhaarValidator} from './validators/aadhar-validator';
import {EmailValidator} from './validators/email-validator';
import {DOBValidator} from './validators/dob-validator';
import { CustomerAdditionalDetailsComponent } from './customer-additional-details/customer-additional-details.component';


export function tokenGetter() {
  return localStorage.getItem("jwt");
}

const appRoutes: Routes = [
  { path: "", component: LandingPageComponent },
  { path: "register-firm", component: FirmRegistrationComponent },
  {
    path: "firm-additional-details/:firmId",
    component: FirmAdditionalDetailsComponent,
  },
  {
    path: "login",
    component: LoginComponent,
  },
  {
    path: "customer-login",
    component: CustomerLoginComponent,
  },
  {
    path:"register-customer",
    component:CustomerRegistrationComponent
  },
  // {
  //   path: "owner-dashboard/:firmId",
  //   component: OwnerDashboardComponent,
  // },
  {
    path: "owner-dashboard/:firmId",
    loadChildren: () =>
      import("./owner-dashboard/owner-dashboard.module").then(
        (m) => m.DashboardModule
      ),
    canActivate: [AuthGuard],
  },
  {
    path:"customer-additional-details/:customerId",
    component:CustomerAdditionalDetailsComponent
  },
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
    NavbarComponent,
    CustomerRegistrationComponent,
    ConfirmPassword,
    AadhaarValidator,
    EmailValidator,
    DOBValidator,
    CustomerAdditionalDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes
      // <-- debugging purposes only
    ),
    BrowserAnimationsModule,
    MatSidenavModule,
    MatMenuModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["http://localhost:5000/"],
        blacklistedRoutes: [],
      },
    }),
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent],
})
export class AppModule {}
