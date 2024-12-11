import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HeaderComponent } from "./Public/header/header.component";
import { LoginComponent } from "./Public/login/login.component";
import { SignUpComponent } from "./Public/sign-up/sign-up.component";
import { ProductsComponent } from "./Public/products/products.component";
import { ProducDetailsComponent } from "./Public/produc-details/produc-details.component";
import { UserManagmentComponent } from "./Admin/user-managment/user-managment.component";
import { UserProfileComponent } from "./Public/user-profile/user-profile.component";
import { CartListComponent } from "./Public/cart-list/cart-list.component";
import { ProductManagmentComponent } from "./Admin/product-managment/product-managment.component";
import { OrdersListComponent } from "./Public/orders-list/orders-list.component";

const routes: Routes = [
  { path: "", component: HeaderComponent },
  { path: "login", component: LoginComponent },
  { path: "signup", component: SignUpComponent },
  { path: "products", component: ProductsComponent },
  { path: "products/:id", component: ProducDetailsComponent },
  { path: "admin/user-managment", component: UserManagmentComponent },
  { path: "user-profile/:id", component: UserProfileComponent },
  { path: "cart-list/:id", component: CartListComponent },
  { path: "admin/product-managment", component: ProductManagmentComponent },
  { path: "orders-list/:id", component: OrdersListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
