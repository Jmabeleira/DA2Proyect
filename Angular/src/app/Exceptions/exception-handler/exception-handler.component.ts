import { Component, Input } from "@angular/core";
import { ExceptionHandlerService } from "src/app/Services/exception-handler.service";

@Component({
  selector: "app-exception-handler",
  templateUrl: "./exception-handler.component.html",
  styleUrls: ["./exception-handler.component.css"],
})
export class ExceptionHandlerComponent {
  @Input() errorMessage: string | null = null;

  constructor(private exceptionHandlerService: ExceptionHandlerService) {
  }
}
