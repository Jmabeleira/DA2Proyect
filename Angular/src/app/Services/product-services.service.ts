import { HttpClient, HttpHeaders } from "@angular/common/http";
import { EventEmitter, Injectable } from "@angular/core";
import { BehaviorSubject, Observable, catchError, map, tap } from "rxjs";
import { Product } from "../Models/product";
import { AuthService } from "./auth.service";
import { environment } from "../Utils/environment.prod";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  private productSubject = new BehaviorSubject<Product[]>([]);
  productList$ = this.productSubject.asObservable();

  productAdded = new EventEmitter<void>();

  private apiUrl = `${environment.apiUrl}/Products`;

  getProducts(
    text?: string | null,
    category?: string | null,
    brand?: string | null,
    minPrice?: number | null,
    maxPrice?: number | null,
    isPromotional?: boolean | null
  ): Observable<Product[]> {
    const params: any = {
      text,
      brand,
      category,
      minPrice: minPrice?.toString() || "",
      maxPrice: maxPrice?.toString() || "",
      isPromotional: isPromotional,
    };
    const query = this.CreateFilterQuery(params);
    return this.http.get<Product[]>(this.apiUrl + query).pipe(
      map((response: any) => {
        return response as Product[];
      })
    );
  }

  getProduct(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`).pipe(
      map((response: any) => {
        return response as Product;
      })
    );
  }

  CreateFilterQuery(params: any) {
    let query = "";
    if (params.text) {
      query += `&text=${params.text}`;
    }
    if (params.brand) {
      query += `&brand=${params.brand}`;
    }
    if (params.category) {
      query += `&category=${params.category}`;
    }
    if (params.minPrice) {
      query += `&minPrice=${params.minPrice}`;
    }
    if (params.maxPrice) {
      query += `&maxPrice=${params.maxPrice}`;
    }
    if (params.isPromotional) {
      query += `&isPromotional=${params.isPromotional}`;
    }
    if (query.length > 0) {
      query = "?" + query.substring(1);
    }

    return query;
  }

  AddProduct(product: any): Observable<Product> {
    return this.http
      .post<Product>(this.apiUrl, product, { observe: "response" })
      .pipe(
        map((response: any) => {
          if (response.status === 201) {
            this.UpdateProductList();
            return response as Product;
          } else {
            throw new Error(`HTTP error ${response.status}`);
          }
        })
      );
  }

  EditProduct(id: string, product: any): Observable<Product> {
    const token = this.authService.getCurrentUser()?.token;
    const headers = new HttpHeaders().set("Authorization", `${token}`);
    const options = { headers: headers, observe: "response" as const };

    return this.http.put<Product>(this.apiUrl, product, options).pipe(
      map((response: any) => {
        if (response.status === 200) {
          this.UpdateProductList();
          return response as Product;
        } else {
          throw new Error(`HTTP error ${response.status}`);
        }
      })
    );
  }

  RemoveProduct(productId: string) {
    let url = this.apiUrl + "/" + productId.toString();

    return this.http.delete(url, { responseType: "text" }).pipe(
      catchError((error) => {
        console.error("Error:", error);
        throw error;
      }),
      tap(() => {
        this.UpdateProductList();
      })
    );
  }

  UpdateProductList() {
    this.getProducts().subscribe((products) => {
      this.productSubject.next(products);
    });
  }
}
