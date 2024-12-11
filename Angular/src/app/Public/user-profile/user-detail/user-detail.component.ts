import { ChangeDetectorRef, Component, Input, OnDestroy } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Subscription } from "rxjs";
import { EditUserComponent } from "src/app/Admin/user-managment/edit-user/edit-user.component";
import { User } from "src/app/Models/user";
import { UserRole } from "src/app/Models/userRole.enum";

@Component({
  selector: "app-user-detail",
  templateUrl: "./user-detail.component.html",
  styleUrls: ["./user-detail.component.css"],
})
export class UserDetailComponent implements OnDestroy {
  @Input() user: User | undefined;
  private modalClosedSubscription: Subscription | undefined;

  constructor(private modalService: NgbModal, private cdr: ChangeDetectorRef) {}

  ngOnDestroy(): void {
    if (this.modalClosedSubscription) {
      this.modalClosedSubscription.unsubscribe();
    }
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

  getBlurredPassword(password: string | undefined): string {
    return password ? "*".repeat(password.length) : "";
  }

  getBlurredToken(token: string | undefined): string {
    return token ? "************" + token.substring(token.length - 4) : "";
  }

  OpenEditUserModal() {
    const modalRef = this.modalService.open(EditUserComponent);
    modalRef.componentInstance.userId = this.user?.id;
    modalRef.componentInstance.InitializeFormGroup();

    this.modalClosedSubscription =
      modalRef.componentInstance.modalClosed.subscribe((data: any) => {
        const updatedUser = data as User;
        this.user = updatedUser;

        this.cdr.detectChanges();
      });
  }
}
