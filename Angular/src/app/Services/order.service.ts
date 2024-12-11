import { Injectable } from "@angular/core";
import { Order } from "../Models/order";
import {
  BehaviorSubject,
  Observable,
  catchError,
  map,
  of,
  throwError,
} from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Cart } from "../Models/cart";
import { environment } from "../Utils/environment.prod";

@Injectable({
  providedIn: "root",
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}/Orders`;
  private ordersSubject = new BehaviorSubject<Order[]>([]);
  private userOrdersSubject = new BehaviorSubject<Order[]>([]);
  ordersList$ = this.ordersSubject.asObservable();
  userOrdersList$ = this.userOrdersSubject.asObservable();

  private cachedOrders: Order[] = [];

  constructor(private http: HttpClient) {}

  getAllOrders(): Observable<Order[]> {
    if (this.cachedOrders.length === 0) {
      return this.http.get<Order[]>(this.apiUrl).pipe(
        map((response: any) => {
          this.cachedOrders = response as Order[];
          return this.cachedOrders;
        })
      );
    } else {
      return of(this.cachedOrders);
    }
  }

  getOrdersByUserId(userId: string) {
    return this.http.get<Order>(this.apiUrl + "/" + userId).pipe(
      map((response: any) => {
        return response as Order[];
      })
    );
  }

  createOrders(order: Order): Observable<Order> {
    return this.http.post(this.apiUrl, order, { observe: "response" }).pipe(
      map((response: any) => {
        if (response.status === 201) {
          this.UpdateOrdersList();
          return response as Order;
        } else {
          throw new Error(`HTTP error ${response.status}`);
        }
      })
    );
  }

  purchase(cartIn: Cart, paymentMethod: string): Observable<Order> {
    const url = `${this.apiUrl}/Purchase?paymentMethod=${paymentMethod}`;

    return this.http.post<Order>(url, cartIn).pipe(
      map((response: any) => {
        this.cachedOrders = [];
        return response as Order;
      })
    );
  }

  UpdateOrdersList() {
    this.cachedOrders = [];
    this.getAllOrders().subscribe((orders) => {
      this.ordersSubject.next(orders);
    });
  }

  UpdateUserOrdersList(userId: string) {
    if (userId) {
      this.getOrdersByUserId(userId).subscribe((userOrders) => {
        this.userOrdersSubject.next(userOrders);
      });
    } else {
      this.userOrdersSubject.next([]);
    }
  }
}
