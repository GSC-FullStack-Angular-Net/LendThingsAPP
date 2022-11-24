import { Injectable } from "@angular/core";
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor,
} from "@angular/common/http";
import { catchError, Observable, throwError } from "rxjs";
import { LoginService } from "../Services/login.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
	constructor(private loginService: LoginService) {}
	intercept(
		request: HttpRequest<any>,
		next: HttpHandler
	): Observable<HttpEvent<any>> {
		return next.handle(request).pipe(
			catchError((err) => {
				if (err.status === 401) {
					// llama a logout si recibimos una respuesta 401 del API
					this.loginService.logout();
					location.reload();
				}
				const error = err.error.message || err.statusText;
				return throwError(error);
			})
		);
	}
}
