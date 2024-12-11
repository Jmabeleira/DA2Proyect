import { Component } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "src/app/Services/auth.service";
import { FormGroup, Validators } from "@angular/forms";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-sign-up",
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.css"],
})
export class SignUpComponent {
  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private exceptionHandler: ExceptionHandlerService
  ) {}
  messageTitle: string = "";
  message: string = "";

  successMessage: Boolean = false;
  form: FormGroup = this.fb.group({
    email: ["", Validators.required],
    password: ["", Validators.required],
    country: ["", Validators.required],
    city: ["", Validators.required],
    doorNumber: ["", Validators.required],
    street: ["", Validators.required],
    postalCode: ["", Validators.required],
  });

  onSubmit() {
    if (this.form.valid) {
      const userData = {
        email: this.form.value.email,
        password: this.form.value.password,
        address: {
          country: this.form.value.country,
          city: this.form.value.city,
          street: this.form.value.street,
          zipCode: this.form.value.postalCode,
          doorNumber: this.form.value.doorNumber,
        },
      };
      this.authService.SignUp(userData).subscribe({
        complete: () => {
          this.router.navigateByUrl("/");
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
    }
  }
}
