import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { NavComponent } from "./Public/nav/nav.component";
import { HeaderComponent } from "./Public/header/header.component";
import { LoginComponent } from "./Public/login/login.component";
import { ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { SignUpComponent } from "./Public/sign-up/sign-up.component";
import { SuccesSignUpComponent } from "./Public/modals/succes-sign-up/succes-sign-up.component";
import { ProductsComponent } from "./Public/products/products.component";
import { ProductItemComponent } from "./Public/products/product-item/product-item.component";
import { ProducDetailsComponent } from "./Public/produc-details/produc-details.component";
import { UserManagmentComponent } from "./Admin/user-managment/user-managment.component";
import { AddUserComponent } from "./Admin/user-managment/add-user/add-user.component";
import { FormsModule } from "@angular/forms";
import { ProductManagmentComponent } from "./Admin/product-managment/product-managment.component";
import { AddproductComponent } from "./Admin/product-managment/addproduct/addproduct.component";
import { RemoveUserComponent } from "./Admin/user-managment/remove-user/remove-user.component";
import { UserProfileComponent } from "./Public/user-profile/user-profile.component";
import { UserDetailComponent } from "./Public/user-profile/user-detail/user-detail.component";
import { CartListComponent } from "./Public/cart-list/cart-list.component";
import { EditUserComponent } from "./Admin/user-managment/edit-user/edit-user.component";
import { EditproductComponent } from "./Admin/product-managment/editproduct/editproduct.component";
import { RemoveproductComponent } from "./Admin/product-managment/removeproduct/removeproduct.component";
import { ExceptionHandlerComponent } from "./Exceptions/exception-handler/exception-handler.component";
import { PurchaseOrderComponent } from "./Public/cart-list/purchase-order/purchase-order.component";
import { OrdersListComponent } from "./Public/orders-list/orders-list.component";
import { ViewOrderDetailComponent } from "./Public/orders-list/view-order-detail/view-order-detail.component";

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HeaderComponent,
    LoginComponent,
    SignUpComponent,
    SuccesSignUpComponent,
    ProductsComponent,
    ProductItemComponent,
    ProducDetailsComponent,
    UserManagmentComponent,
    AddUserComponent,
    RemoveUserComponent,
    UserProfileComponent,
    UserDetailComponent,
    CartListComponent,
    EditUserComponent,
    PurchaseOrderComponent,
    ProductManagmentComponent,
    AddproductComponent,
    EditproductComponent,
    RemoveproductComponent,
    ExceptionHandlerComponent,
    OrdersListComponent,
    ViewOrderDetailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
