import { Component } from "@angular/core";
import { Product } from "src/app/Models/product";
import { Input } from "@angular/core";
import { AuthService } from "src/app/Services/auth.service";
import { CartService } from "src/app/Services/cart.service";
import { map, switchMap } from "rxjs";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-product-item",
  templateUrl: "./product-item.component.html",
  styleUrls: ["./product-item.component.css"],
})
export class ProductItemComponent {
  @Input() product: Product | undefined;

  constructor(
    public authService: AuthService,
    public cartService: CartService,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  ngOnInit(): void {}

  addToCart() {
    const userId = this.authService.getCurrentUser()?.id;
    this.cartService
      .getCartByUserId(userId!)
      .pipe(
        switchMap((cart) => {
          return this.cartService
            .addProduct(cart.id!, this.product!)
            .pipe(map(() => cart));
        })
      )
      .subscribe({
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
  }
}
