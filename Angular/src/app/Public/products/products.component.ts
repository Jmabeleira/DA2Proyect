import { Component } from "@angular/core";
import { AuthService } from "src/app/Services/auth.service";
import { ProductService } from "src/app/Services/product-services.service";
import { Product } from "src/app/Models/product";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["./products.component.css"],
})
export class ProductsComponent {
  constructor(
    private productService: ProductService,
    private authService: AuthService,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  products: Product[] = [];
  searchText: string = "";
  selectedBrand: string = "";
  category: string = "";
  minPrice: number | null = null;
  maxPrice: number | null = null;
  isPromotional: boolean = false;
  filteredProducts: Product[] = [];

  applyFilters() {
    this.productService
      .getProducts(
        this.searchText,
        this.category,
        this.selectedBrand,
        this.minPrice,
        this.maxPrice,
        this.isPromotional
      )
      .subscribe({
        next: (filteredProducts) => {
          this.products = filteredProducts;
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
  }

  ngOnInit(): void {
    this.productService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
      },
      error: (error) => {
        this.exceptionHandler.handleException(error);
      },
    });
  }

  isLoggedIn(): boolean {
    return this.authService.loggedIn;
  }
}
