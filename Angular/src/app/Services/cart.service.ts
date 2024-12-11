import { Injectable } from "@angular/core";
import { Product } from "src/app/Models/product";
import { HttpClient } from "@angular/common/http";
import {
  BehaviorSubject,
  Observable,
  catchError,
  map,
  switchMap,
  tap,
  throwError,
} from "rxjs";
import { Cart } from "../Models/cart";
import { UserService } from "./user.service";
import { User } from "../Models/user";
import { CartIn } from "../Models/in/cartIn";
import { CartProduct } from "../Models/cartProduct";
import { ExceptionHandlerService } from "./exception-handler.service";
import { environment } from "../Utils/environment.prod";

@Injectable({
  providedIn: "root",
})
export class CartService {
  private apiUrl = `${environment.apiUrl}/Carts`;
  private cartSubject = new BehaviorSubject<Cart | null>(null);
  cart$ = this.cartSubject.asObservable();
  private cartProductsSubject = new BehaviorSubject<CartProduct[]>([]);
  cartProducts$ = this.cartProductsSubject.asObservable();

  constructor(
    private http: HttpClient,
    private userService: UserService,
    public exceptionHandler: ExceptionHandlerService
  ) {}

  addCart(userId: string): Observable<Cart> {
    const url = `${this.apiUrl}`;

    return this.userService.getUserById(userId).pipe(
      catchError((error) => {
        console.error("Error fetching user:", error);
        return throwError("Error al cargar el usuario");
      }),
      switchMap((userResponse: User) => {
        const cartIn: CartIn = {
          user: userResponse,
          cartProducts: [],
        };
        return this.http.post<Cart>(url, { cartIn: cartIn }).pipe(
          map((response: any) => {
            this.notifyCartByUserId(userResponse.id!);
            return response as Cart;
          }),
          catchError((error: any) => {
            console.error("Error fetching cart:", error);
            return throwError(error);
          })
        );
      })
    );
  }

  getCartById(cartId: string): Observable<Cart> {
    const url = `${this.apiUrl}/${cartId}`;

    return this.http.get<Cart>(url).pipe(
      map((response: any) => {
        return response as Cart;
      }),
      catchError((error: any) => {
        console.error("Error fetching cart:", error);
        return throwError(error);
      })
    );
  }

  getCartByUserId(userId: string): Observable<Cart> {
    const url = `${this.apiUrl}?UserId=${userId}`;

    return this.http.get<Cart>(url).pipe(
      map((response: any) => {
        return response as Cart;
      }),
      catchError((error: any) => {
        console.error("Error fetching cart:", error);
        return this.addCart(userId);
      })
    );
  }

  addProduct(cartId: string, product: Product) {
    const url = `${this.apiUrl}/AddProduct/${cartId}`;

    return this.http.post(url, product).pipe(
      switchMap(() => this.getCartById(cartId)),
      tap(() => this.notifyCartById(cartId)),
      catchError((error: any) => {
        console.error("Error fetching cart:", error);
        return throwError(error);
      })
    );
  }

  modifyCartById(cartId: string, cart: Cart) {
    const url = `${this.apiUrl}/${cartId}`;

    return this.http.put(url, cart, { observe: "response" }).pipe(
      map((response: any) => {
        if (response.status === 200) {
          this.notifyCartByUserId(cart.user.id!);
          return response as Cart;
        } else {
          throw new Error(`HTTP error ${response.status}`);
        }
      })
    );
  }

  deleteCart(cart: Cart) {
    const url = `${this.apiUrl}/${cart.id}`;

    return this.http.delete(url, { responseType: "text" }).pipe(
      catchError((error) => {
        this.exceptionHandler.handleException(error.error.errorMessage);
        throw error;
      }),
      tap(() => {
        this.notifyCartByUserId(cart.user.id!);
      })
    );
  }

  notifyCartByUserId(userId: string) {
    this.getCartByUserId(userId).subscribe((cart) => {
      this.cartSubject.next(cart);
      this.cartProductsSubject.next(cart!.products);
    });
  }

  notifyCartById(cartId: string) {
    this.getCartById(cartId).subscribe((cart) => {
      this.cartSubject.next(cart);
      this.cartProductsSubject.next(cart!.products);
    });
  }
}
