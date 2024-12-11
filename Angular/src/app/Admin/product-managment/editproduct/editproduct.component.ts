import { Component, Input } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { of, switchMap } from "rxjs";
import { Product } from "src/app/Models/product";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";
import { ProductService } from "src/app/Services/product-services.service";

@Component({
  selector: "app-editproduct",
  templateUrl: "./editproduct.component.html",
  styleUrls: ["./editproduct.component.css"],
})
export class EditproductComponent {
  @Input() productId: string = "";
  @Input() product: Product | undefined;
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

  InitializeFormGroup(): void {
    this.productService
      .getProduct(this.productId)
      .pipe(
        switchMap((product) => {
          const prod = {
            productName: product?.name,
            productPrice: product?.price,
            productDescription: product?.description,
            productBrand: product?.brand,
            productColors: this.printColors(product?.colors),
            productCategory: product?.category,
            productStock: product?.stock,
            isPromotional: product?.isPromotional,
          };
          this.form.setValue(prod);
          return of(prod);
        })
      )
      .subscribe((product: any) => {
        this.product = product;
      });
  }

  EditProduct() {
    this.form.markAllAsTouched();
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
        id: this.productId,
        name: this.form.value.productName,
        price: this.form.value.productPrice,
        description: this.form.value.productDescription,
        brand: this.form.value.productBrand,
        colors: colorArray,
        category: this.form.value.productCategory,
        stock: this.form.value.productStock,
        isPromotional: Promotional,
      };
      this.productService.EditProduct(this.productId, productData).subscribe({
        complete: () => {
          this.activeModal.close("Edit");
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
    }
  }

  printColors(colors: string[]): string {
    let result = "";
    colors.forEach((color) => {
      result += color + "\n";
    });
    return result;
  }
}
