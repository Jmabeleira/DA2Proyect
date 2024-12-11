import { Component } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Product } from "src/app/Models/product";
import { ProductService } from "src/app/Services/product-services.service";
import { AddproductComponent } from "./addproduct/addproduct.component";
import { EditproductComponent } from "./editproduct/editproduct.component";
import { RemoveproductComponent } from "./removeproduct/removeproduct.component";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-product-managment",
  templateUrl: "./product-managment.component.html",
  styleUrls: ["./product-managment.component.css"],
})
export class ProductManagmentComponent {
  constructor(
    public productService: ProductService,
    private modalService: NgbModal,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  products: Product[] = [];
  selectedRadioValue: string = "";

  ngOnInit(): void {
    this.productService.UpdateProductList();
  }

  EditProduct(id: string) {
    const modalRef = this.modalService.open(EditproductComponent);
    modalRef.componentInstance.productId = id;
    modalRef.componentInstance.InitializeFormGroup();
  }

  RemoveProduct(id: string) {
    const modalRef = this.modalService.open(RemoveproductComponent);
    modalRef.componentInstance.SetUserId(id);
  }

  OpenModal() {
    const modalRef = this.modalService.open(AddproductComponent);
    modalRef.result.then((result) => {
      this.handleModalClose(result);
    });

    this.productService.getProducts().subscribe({
      next: (products: Product[]) => {
        this.products = products;
      },
      error: (error) => {
        this.exceptionHandler.handleException(error);
      },
    });
  }

  private handleModalClose(result: any) {
    this.productService.getProducts().subscribe((products) => {
      this.products = products;
    });
  }
}
