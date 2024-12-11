import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/Services/auth.service";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"],
})
export class NavComponent {
  constructor(public authService: AuthService, private router: Router) {}

  LogOut() {
    if (this.authService.getCurrentUser() != null) {
      this.authService.LogOut();
      this.router.navigateByUrl("/login");
    }
  }
}
