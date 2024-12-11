import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { UserService } from "src/app/Services/user.service";
import { Utils } from "src/app/Utils/Utils";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-add-user",
  templateUrl: "./add-user.component.html",
  styleUrls: ["./add-user.component.css"],
})
export class AddUserComponent {
  constructor(
    public userService: UserService,
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  form: FormGroup = this.fb.group({
    email: ["", Validators.required],
    password: ["", Validators.required],
    country: ["", Validators.required],
    city: ["", Validators.required],
    doorNumber: ["", Validators.required],
    street: ["", Validators.required],
    postalCode: ["", Validators.required],
    userRole: ["", Validators.required],
  });

  AddUser() {
    if (this.form.valid) {
      const userData = {
        email: this.form.value.email,
        password: this.form.value.password,
        userRole: Utils.MapUserRole(this.form.value.userRole),
        address: {
          country: this.form.value.country,
          city: this.form.value.city,
          street: this.form.value.street,
          zipCode: this.form.value.postalCode,
          doorNumber: this.form.value.doorNumber,
        },
      };

      this.userService.createUser(userData).subscribe({
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
