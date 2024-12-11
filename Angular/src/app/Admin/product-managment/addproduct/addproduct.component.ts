import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { ProductService } from "src/app/Services/product-services.service";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-addproduct",
  templateUrl: "./addproduct.component.html",
  styleUrls: ["./addproduct.component.css"],
})
export class AddproductComponent {
  selectedRadioValue: string = "";
  constructor(
    public productService: ProductService,
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  form: FormGroup = this.fb.group({
    isPromotional: [""],
    productColors: [""],
    productName: ["", Validators.required],
    productPrice: ["", Validators.required],
    productStock: ["", Validators.required],
    productDescription: ["", Validators.required],
    productCategory: ["", Validators.required],
    productBrand: ["", Validators.required],
  });

  AddProduct() {
    var Promotional = false;
    const control = this.form.get("isPromotional");
    if (control) {
      const isPromotionalValue = control.value;
      if (isPromotionalValue === "promotional") {
        Promotional = true;
      }
    }
    if (this.form.valid) {
      const colorInput: string = this.form.value.productColors;
      const colorArray = colorInput
        .split("\n")
        .map((color: string) => color.trim());
      const productData = {
        name: this.form.value.productName,
        price: this.form.value.productPrice,
        description: this.form.value.productDescription,
        brand: this.form.value.productBrand,
        colors: colorArray,
        category: this.form.value.productCategory,
        stock: this.form.value.productStock,
        isPromotional: Promotional,
      };

      this.productService.AddProduct(productData).subscribe({
        complete: () => {
          this.activeModal.close();
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
    }
  }
}
