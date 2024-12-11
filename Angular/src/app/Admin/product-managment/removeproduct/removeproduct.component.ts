import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";
import { ProductService } from "src/app/Services/product-services.service";

@Component({
  selector: "app-removeproduct",
  templateUrl: "./removeproduct.component.html",
  styleUrls: ["./removeproduct.component.css"],
})
export class RemoveproductComponent {
  productId: string = "";
  constructor(
    public productService: ProductService,
    public activeModal: NgbActiveModal,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  RemoveProduct() {
    this.productService.RemoveProduct(this.productId).subscribe({
      error: (error) => {
        this.exceptionHandler.handleException(error);
      },
    });
    this.activeModal.close();
  }

  SetUserId(id: string) {
    this.productId = id;
  }
}
