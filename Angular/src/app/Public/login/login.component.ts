import { Component } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "src/app/Services/auth.service";
import { FormGroup, Validators } from "@angular/forms";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent {
  form: FormGroup = this.fb.group({
    email: ["", Validators.required],
    password: ["", Validators.required],
  });
  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private exceptionHandler: ExceptionHandlerService
  ) {}
  user: any = null;
  Login() {
    this.authService
      .LogIn(this.form.value.email, this.form.value.password)
      .subscribe({
        next: (User) => {
          this.user = User;
          this.authService.loggedIn = true;
          if (this.user.role == "Admin") {
            this.router.navigateByUrl("/");
          } else {
            this.router.navigateByUrl("/");
          }
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
  }

  getCurrentUser() {
    this.user = this.authService.getCurrentUser();
  }
}
