import { Component, OnInit } from "@angular/core";
import { CartService } from "../../Services/cart.service";
import { ActivatedRoute } from "@angular/router";
import { Cart } from "src/app/Models/cart";
import { CartProduct } from "src/app/Models/cartProduct";
import { switchMap, tap } from "rxjs";
import { User } from "src/app/Models/user";
import { PurchaseOrderComponent } from "./purchase-order/purchase-order.component";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";
import { AuthService } from "src/app/Services/auth.service";
import { ExceptionHandlerComponent } from "src/app/Exceptions/exception-handler/exception-handler.component";

@Component({
  selector: "app-cart-list",
  templateUrl: "./cart-list.component.html",
  styleUrls: ["./cart-list.component.css"],
})
export class CartListComponent implements OnInit {
  cart: Cart | undefined;
  cartProducts: CartProduct[] = [];
  userId: string | null = "";
  user: User | undefined;

  constructor(
    public cartService: CartService,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private exceptionHandler: ExceptionHandlerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.route.paramMap
      .pipe(
        tap((params) => (this.userId = params.get("id"))),
        switchMap(() =>
          this.cartService.getCartByUserId(this.userId ? this.userId : "")
        ),
        switchMap((cart: Cart) => {
          this.cartService.notifyCartByUserId(this.userId ? this.userId : "");
          return this.cartService.getCartById(cart?.id ? cart.id : "");
        })
      )
      .subscribe({
        next: (updatedCart: Cart) => {
          this.cartProducts = updatedCart?.products!;
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });

    this.cartService.cart$.subscribe((cart) => {
      this.cart = cart!;
    });

    this.cartService.cartProducts$.subscribe((cartProducts) => {
      this.cartProducts = cartProducts;
    });

    this.user = this.authService.getCurrentUser()!;
  }
  calculatePromoDiscount(): number {
    if (this.cart && this.cart.promotion) {
      const totalPrice = this.cartProducts.reduce((total, cartProduct) => {
        const productPrice = cartProduct.product.price * cartProduct.quantity;
        return total + productPrice;
      }, 0);

      const promoDiscount = totalPrice - this.cart.totalPrice;

      return promoDiscount;
    } else {
      return 0;
    }
  }

  updateOwnerAndDeletePromo() {
    if (this.cart?.user != null && this.user != null) {
      this.cart.user = this.user;
      this.cart.promotion = null;
    }
  }

  editCartItem(cartProductToUpdateId: string, quantity: string) {
    if (this.cart) {
      this.updateOwnerAndDeletePromo();
      this.cart.products = this.cart.products.map((cartProduct) => {
        if (cartProduct.id === cartProductToUpdateId) {
          cartProduct.quantity = parseInt(quantity);
        }
        return cartProduct;
      });
      this.cartService.modifyCartById(this.cart.id!, this.cart).subscribe({
        complete: () => {
          this.cartService.notifyCartByUserId(this.userId!);
        },
        error: (error) => {
          let modalRef = this.modalService.open(ExceptionHandlerComponent);
          modalRef.componentInstance.errorMessage = error.error.errorMessage;
        },
      });
    }
  }

  deleteCartItem(cartProductToDelete: CartProduct) {
    if (this.cart) {
      this.updateOwnerAndDeletePromo();
      this.cart.products = this.cart.products.filter(
        (cartProduct) => cartProduct.id !== cartProductToDelete.id
      );
      this.cartService.modifyCartById(this.cart.id!, this.cart).subscribe({
        complete: () => {
          this.cartService.notifyCartByUserId(this.userId!);
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
    }
  }

  purchaseCart() {
    const modalRef = this.modalService.open(PurchaseOrderComponent);
    modalRef.componentInstance.cart = this.cart;
  }
}
