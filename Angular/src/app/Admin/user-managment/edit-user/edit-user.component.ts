import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { of, switchMap } from "rxjs";
import { User } from "src/app/Models/user";
import { UserRole } from "src/app/Models/userRole.enum";
import { AuthService } from "src/app/Services/auth.service";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";
import { UserService } from "src/app/Services/user.service";
import { Utils } from "src/app/Utils/Utils";

@Component({
  selector: "app-edit-user",
  templateUrl: "./edit-user.component.html",
  styleUrls: ["./edit-user.component.css"],
})
export class EditUserComponent implements OnInit {
  @Input() userId: string = "";
  @Input() user: User | undefined;
  @Output() modalClosed: EventEmitter<User> = new EventEmitter<User>();
  userLogged: User | null = null;
  constructor(
    private userService: UserService,
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private exceptionHandler: ExceptionHandlerService,
    private authService: AuthService
  ) {}

  form: FormGroup = this.fb.group({
    email: ["", Validators.required],
    password: ["", Validators.required],
    country: ["", Validators.required],
    city: ["", Validators.required],
    doorNumber: ["", Validators.required],
    street: ["", Validators.required],
    postalCode: ["", Validators.required],
    userRole: [Validators.required],
  });

  ngOnInit(): void {
    if (!this.userLogged) {
      this.userLogged = this.authService.getCurrentUser();
    }
  }

  InitializeFormGroup(): void {
    this.userService
      .getUserById(this.userId)
      .pipe(
        switchMap((userResponse: any) => {
          const userData = {
            email: userResponse?.email,
            password: userResponse?.password,
            userRole: this.UserRoleToFrontMap(userResponse?.userRole!),
            country: userResponse?.address?.country,
            city: userResponse?.address?.city,
            street: userResponse?.address?.street,
            postalCode: userResponse?.address?.zipCode,
            doorNumber: userResponse?.address?.doorNumber,
          };

          this.form.setValue(userData);
          return of(userResponse);
        })
      )
      .subscribe((user: any) => {
        this.user = user;
      });
  }

  EditUser() {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      const userData = {
        email: this.form.value.email,
        password: this.form.value.password,
        UserRole: Utils.MapUserRole(this.form.value.userRole),
        token: this.user?.token!,
        address: {
          country: this.form.value.country,
          city: this.form.value.city,
          street: this.form.value.street,
          zipCode: this.form.value.postalCode,
          doorNumber: this.form.value.doorNumber,
        },
        timestamp: new Date(),
      };

      this.userService
        .EditUser(this.user!.id, userData, this.isAdmin())
        .subscribe(
          (response: any) => {
            const updatedUser = response.body as User;
            this.modalClosed.emit(updatedUser);
            this.activeModal.close();
          },
          (error) => {
            this.exceptionHandler.handleException(error);
          }
        );
    }
  }

  UserRoleToFrontMap(role: UserRole[]) {
    if (
      role?.some((x) => x == UserRole.Admin) &&
      !role?.some((x) => x == UserRole.Client)
    ) {
      return 0;
    }
    if (
      role?.some((x) => x == UserRole.Client) &&
      !role?.some((x) => x == UserRole.Admin)
    ) {
      return 1;
    }

    return 2;
  }

  isAdmin() {
    if (this.userLogged != null) {
      return this.userLogged.userRole.some((x) => x == 0);
    }
    return false;
  }

  getRoleString(role: UserRole): string {
    switch (role) {
      case UserRole.Admin:
        return "Admin";
      case UserRole.Client:
        return "Client";
      default:
        return "Unknown";
    }
  }
}
