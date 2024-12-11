import { Component, ViewChild } from "@angular/core";
import { RouterModule } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { UserService } from "src/app/Services/user.service";
import { AddUserComponent } from "./add-user/add-user.component";
import { RemoveUserComponent } from "./remove-user/remove-user.component";
import { EditUserComponent } from "./edit-user/edit-user.component";

@Component({
  selector: "app-user-managment",
  templateUrl: "./user-managment.component.html",
  styleUrls: ["./user-managment.component.css"],
})
export class UserManagmentComponent {
  @ViewChild(RemoveUserComponent) removeUserComponent!: RemoveUserComponent;
  constructor(
    public userService: UserService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.userService.UpdateUsersList();
  }

  EditUser(id: string) {}

  RemoveUser(id: string) {}

  OpenAddUserModal() {
    const modalRef = this.modalService.open(AddUserComponent);
  }

  OpenRemoveUserModal(userId: string) {
    const modalRef = this.modalService.open(RemoveUserComponent);
    modalRef.componentInstance.SetUserId(userId);
  }

  OpenEditUserModal(userId: string) {
    const modalRef = this.modalService.open(EditUserComponent);
    modalRef.componentInstance.userId = userId;
    modalRef.componentInstance.InitializeFormGroup();
  }
}
