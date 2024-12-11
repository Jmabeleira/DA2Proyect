import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { catchError, throwError } from "rxjs";
import { User } from "src/app/Models/user";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";
import { UserService } from "src/app/Services/user.service";

@Component({
  selector: "app-user-profile",
  templateUrl: "./user-profile.component.html",
  styleUrls: ["./user-profile.component.css"],
})
export class UserProfileComponent implements OnInit {
  userId: string | null = null;
  user: User | undefined;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private exceptionHandler: ExceptionHandlerService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.userId = params.get("id");
    });

    if (this.userId != null) {
      this.userService.getUserById(this.userId).subscribe({
        next: (user: User) => {
          this.user = user;
        },
        error: (error) => {
          this.exceptionHandler.handleException(error);
        },
      });
    }
  }
}
