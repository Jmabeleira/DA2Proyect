import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { User } from "../Models/user";
import { environment } from "../Utils/environment.prod";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private http: HttpClient) {
    this.StartSession();
  }
  session: any = null;
  loggedIn: boolean = false;
  isAdmin: boolean = false;

  getCurrentUser(): User | null {
    const currentUserString = localStorage.getItem("currentUser");
    return currentUserString ? JSON.parse(currentUserString) : null;
  }

  LogIn(email: string, password: string): Observable<User> {
    return this.http
      .post(
        `${environment.apiUrl}/Auths/login`,
        { email: email, password: password },
        { observe: "response" }
      )
      .pipe(
        map((response) => {
          localStorage.setItem(
            "currentUser",
            JSON.stringify(response.body as User)
          );
          let userLoggedIn = response.body as User;
          this.loggedIn = true;
          this.IsAdmin();
          return userLoggedIn;
        })
      );
  }

  LogOut() {
    localStorage.removeItem("currentUser");
    this.loggedIn = false;
    this.session = null;
    this.isAdmin = false;
    this.http.post(`${environment.apiUrl}/Auths/logout`, {});
  }

  SignUp(user: any): Observable<User> {
    return this.http
      .post(`${environment.apiUrl}/Auths/register`, user, {
        observe: "response",
      })
      .pipe(
        map((response) => {
          var user = response.body as User;
          localStorage.setItem(
            "currentUser",
            JSON.stringify(response.body as User)
          );
          this.loggedIn = true;
          return user as User;
        })
      );
  }

  StartSession() {
    let user = this.getCurrentUser();
    if (user != null) {
      this.loggedIn = true;
      this.isAdmin = this.IsAdmin();
    }
  }

  IsAdmin() {
    let user = this.getCurrentUser();
    if (user != null) {
      return user.userRole.some((x) => x == 0);
    }
    return false;
  }
}
