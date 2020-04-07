import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { OwnerDashboardComponent } from "./owner-dashboard.component";
import { RouterModule } from "@angular/router";
import { OwnerDashboardDetailsComponent } from "../owner-dashboard-details/owner-dashboard-details.component";
import { CustomersListComponent } from "../customers-list/customers-list.component";
import { CreateCustomerComponent } from "../create-customer/create-customer.component";
import { CreateAgreementComponent } from "../create-agreement/create-agreement.component";
import { ActiveAccountsComponent } from "../active-accounts/active-accounts.component";
import { LoanTypesComponent } from "../loan-types/loan-types.component";

@NgModule({
  declarations: [
    OwnerDashboardComponent,
    OwnerDashboardDetailsComponent,
    CustomersListComponent,
    CreateCustomerComponent,
    CreateAgreementComponent,
    ActiveAccountsComponent,
    LoanTypesComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: "",
        component: OwnerDashboardComponent,
        children: [
          {
            path: "dashboard",
            component: OwnerDashboardDetailsComponent,
          },
          {
            path: "customers-list",
            component: CustomersListComponent,
          },
          {
            path: "create-customer",
            component: CreateCustomerComponent,
          },
          {
            path: "create-agreement",
            component: CreateAgreementComponent,
          },
          {
            path: "active-accounts",
            component: ActiveAccountsComponent,
          },
          {
            path: "loan-types",
            component: LoanTypesComponent,
          },
        ],
      },
    ]),
  ],
})
export class DashboardModule {}
