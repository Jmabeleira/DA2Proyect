import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { Cart } from "src/app/Models/cart";
import { AuthService } from "src/app/Services/auth.service";
import { CartService } from "src/app/Services/cart.service";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";
import { OrderService } from "src/app/Services/order.service";

@Component({
  selector: "app-purchase-order",
  templateUrl: "./purchase-order.component.html",
  styleUrls: ["./purchase-order.component.css"],
})
export class PurchaseOrderComponent {
  cart: Cart | undefined;
  paymentForm: FormGroup;

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private orderService: OrderService,
    private authService: AuthService,
    private cartService: CartService,
    private exceptionHandler: ExceptionHandlerService
  ) {
    this.paymentForm = this.fb.group({
      paymentMethod: ["", Validators.required],
      creditCardType: [""],
      debitCardType: [""],
    });
  }

  paymentMethods = [
    { type: "CreditCard", displayName: "Credit Card" },
    { type: "DebitCard", displayName: "Debit Card" },
    { type: "Paganza", displayName: "Paganza" },
    { type: "PayPal", displayName: "PayPal" },
  ];
  creditCardTypes = ["Visa", "MasterCard"];
  debitCardTypes = ["Santander", "Itau", "Bbva"];

  isCreditCardSelected(): boolean {
    return this.paymentForm?.get("paymentMethod")?.value === "CreditCard";
  }

  isDebitCardSelected(): boolean {
    return this.paymentForm?.get("paymentMethod")?.value === "DebitCard";
  }

  getCartUser() {
    if (this.cart?.user != null) {
      this.cart!.user = this.authService.getCurrentUser()!;
    }
  }

  purchase() {
    this.getCartUser();
    const selectedPaymentType = this.paymentForm
      .get("paymentMethod")
      ?.value.replace(/\s/g, "");
    const selectedCreditCardType =
      this.paymentForm.get("creditCardType")?.value;
    const selectedDebitCardType = this.paymentForm.get("debitCardType")?.value;

    if (selectedPaymentType) {
      let paymentMethod = selectedPaymentType;
      if (selectedPaymentType === "CreditCard" && selectedCreditCardType) {
        paymentMethod += selectedCreditCardType;
      }
      if (selectedPaymentType === "DebitCard" && selectedDebitCardType) {
        paymentMethod += selectedDebitCardType;
      }

      this.orderService.purchase(this.cart!, paymentMethod).subscribe({
        complete: () => {
          this.cartService.deleteCart(this.cart!).subscribe();
          this.activeModal.close("Success");
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
          this.activeModal.close("Error");
        },
      });
    }
  }

  calculateDiscountedPrice(): string | number {
    const totalPrice = this.cart?.totalPrice;
    const discountedPrice = totalPrice! - totalPrice! * 0.1;
    return discountedPrice;
  }
}
