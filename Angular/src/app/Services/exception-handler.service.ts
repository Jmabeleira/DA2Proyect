import { HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ExceptionHandlerService {
  constructor() {}

  private errorSubject = new Subject<string>();
  errorMessage$ = this.errorSubject.asObservable();

  handleException(error: unknown) {
    const errorMessage =
      error instanceof HttpErrorResponse
        ? error.error.errorMessage
        : "Unknown error";
    this.errorSubject.next(errorMessage);
  }
}
