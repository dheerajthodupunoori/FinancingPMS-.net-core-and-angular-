import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { FormsModule } from "@angular/forms";
import { FooterComponent } from "./footer/footer.component";
import { FirmRegistrationComponent } from "./firm-registration/firm-registration.component";
import { LandingPageComponent } from "./landing-page/landing-page.component";
import { FirmDetailsComponent } from './firm-details/firm-details.component';

const appRoutes: Routes = [
  { path: "", component: LandingPageComponent },
  { path: "register-firm", component: FirmRegistrationComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    FirmRegistrationComponent,
    LandingPageComponent,
    FirmDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(
      appRoutes
      // <-- debugging purposes only
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
