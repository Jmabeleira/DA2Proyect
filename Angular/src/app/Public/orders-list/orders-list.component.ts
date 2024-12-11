import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { switchMap, tap } from "rxjs";
import { Order } from "src/app/Models/order";
import { User } from "src/app/Models/user";
import { OrderService } from "src/app/Services/order.service";
import { ViewOrderDetailComponent } from "./view-order-detail/view-order-detail.component";
import { AuthService } from "src/app/Services/auth.service";

@Component({
  selector: "app-orders-list",
  templateUrl: "./orders-list.component.html",
  styleUrls: ["./orders-list.component.css"],
})
export class OrdersListComponent {
  allOrders: Order[] = [];
  userOrders: Order[] = [];
  userId: string | null = "";
  user: User | null = null;

  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.user = this.authService.getCurrentUser();
    if (this.isAdmin()) {
      this.orderService.getAllOrders().subscribe((orders: Order[]) => {
        this.allOrders = orders;
      });
    }

    this.route.paramMap
      .pipe(
        tap((params) => (this.userId = params.get("id"))),
        switchMap(() =>
          this.orderService.getOrdersByUserId(this.userId ? this.userId : "")
        ),
        tap((orders: Order[]) => {
          this.userOrders = orders;
        })
      )
      .subscribe();
  }

  isAdmin(): boolean {
    if (this.user != null) {
      return this.user.userRole.some((x) => x == 0);
    }
    return false;
  }

  viewDetails(order: Order) {
    const modalRef = this.modalService.open(ViewOrderDetailComponent);
    modalRef.componentInstance.order = order;
  }
}
