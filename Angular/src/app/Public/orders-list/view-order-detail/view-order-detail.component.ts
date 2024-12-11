import { Component } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { Order } from "src/app/Models/order";

@Component({
  selector: "app-view-order-detail",
  templateUrl: "./view-order-detail.component.html",
  styleUrls: ["./view-order-detail.component.css"],
})
export class ViewOrderDetailComponent {
  order: Order | undefined;

  constructor(public activeModal: NgbActiveModal) {}

  closeModal() {
    this.activeModal.close("Error");
  }
}
