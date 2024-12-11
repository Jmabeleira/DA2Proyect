import { Component, Input } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-succes-sign-up",
  templateUrl: "./succes-sign-up.component.html",
  styleUrls: ["./succes-sign-up.component.css"],
})
export class SuccesSignUpComponent {
  @Input() messageTitle: string = "";
  @Input() message: string = "";

  constructor(private modal: NgbModal) {}
  openModal() {
    const modalRef = this.modal.open(SuccesSignUpComponent);
    modalRef.componentInstance.messageTitle = this.messageTitle;
    modalRef.componentInstance.message = this.message;
  }
  close() {
    const modal = document.getElementById("container");
    if (modal != null) {
      modal.style.display = "none";
    }
  }
}
