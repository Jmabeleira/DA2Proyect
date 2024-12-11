import { Component, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";
import { UserService } from "src/app/Services/user.service";

@Component({
  selector: "app-remove-user",
  templateUrl: "./remove-user.component.html",
  styleUrls: ["./remove-user.component.css"],
})
export class RemoveUserComponent {
  userId: string = "";
  constructor(
    public userService: UserService,
    public activeModal: NgbActiveModal,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  RemoveUser() {
    this.userService.RemoveUser(this.userId).subscribe({
      complete: () => {
        this.activeModal.close();
      },
      error: (error) => {
        this.exceptionHandler.handleException(error);
      },
    });
  }

  SetUserId(userId: string) {
    this.userId = userId;
  }
}
