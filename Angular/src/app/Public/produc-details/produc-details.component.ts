import { Component, Input } from "@angular/core";
import { ProductService } from "src/app/Services/product-services.service";
import { Product } from "src/app/Models/product";
import { ActivatedRoute } from "@angular/router";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-produc-details",
  templateUrl: "./produc-details.component.html",
  styleUrls: ["./produc-details.component.css"],
})
export class ProducDetailsComponent {
  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private exceptionHandler: ExceptionHandlerService
  ) {
    this.productId = this.route.snapshot.params["id"];
  }
  productId: string = "";
  product!: Product;

  ngOnInit(): void {
    this.productService.getProduct(this.productId).subscribe({
      next: (product) => {
        this.product = product;
      },
      error: (error) => {
        this.exceptionHandler.handleException(error);
      },
    });
  }
}
