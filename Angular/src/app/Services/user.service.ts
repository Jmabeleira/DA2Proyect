import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from "@angular/common/http";
import { EventEmitter, Injectable } from "@angular/core";
import { User } from "../Models/user";
import { BehaviorSubject, Observable, catchError, map, tap } from "rxjs";
import { ExceptionHandlerService } from "./exception-handler.service";
import { AuthService } from "./auth.service";
import { environment } from "../Utils/environment.prod";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private usersSubject = new BehaviorSubject<User[]>([]);
  usersList$ = this.usersSubject.asObservable();

  userAdded = new EventEmitter<void>();
  constructor(
    private http: HttpClient,
    public exceptionHandler: ExceptionHandlerService,
    private authService: AuthService
  ) {}

  private apiUrl = `${environment.apiUrl}/Users`;

  getAllUsers() {
    return this.http.get<User[]>(this.apiUrl).pipe(
      map((response: any) => {
        return response as User[];
      })
    );
  }

  getUserById(id: string) {
    return this.http.get<User>(this.apiUrl + "/" + id).pipe(
      map((response: any) => {
        return response as User;
      })
    );
  }

  createUser(user: any): Observable<User> {
    const token = this.authService.getCurrentUser()?.token;
    const headers = new HttpHeaders().set("Authorization", `${token}`);
    const options = { headers: headers, observe: "response" as const };
    return this.http.post(this.apiUrl, user, options).pipe(
      map((response: any) => {
        this.UpdateUsersList();
        return response as User;
      })
    );
  }

  UpdateUsersList() {
    this.getAllUsers().subscribe((users) => {
      this.usersSubject.next(users);
    });
  }

  RemoveUser(userId: string) {
    let url = this.apiUrl + "/" + userId.toString();
    return this.http.delete(url, { responseType: "text" }).pipe(
      tap(() => {
        this.UpdateUsersList();
      })
    );
  }

  EditUser(id: string, userIn: any, isAdmin: boolean = true): Observable<User> {
    const token = this.authService.getCurrentUser()?.token;
    const headers = new HttpHeaders().set("Authorization", `${token}`);
    const options = { headers: headers, observe: "response" as const };

    return this.http.put(this.apiUrl + "/" + id, userIn, options).pipe(
      map((response: any) => {
        if (response instanceof HttpErrorResponse) {
          this.exceptionHandler.handleException(response.error.errorMessage);
          throw response;
        } else {
          if (isAdmin) {
            this.UpdateUsersList();
          }
          return response as User;
        }
      })
    );
  }
}
