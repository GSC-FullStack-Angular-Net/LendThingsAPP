import { Injectable } from "@angular/core";
import {
	ActivatedRouteSnapshot,
	CanActivate,
	Router,
	RouterStateSnapshot,
	UrlTree,
} from "@angular/router";
import { Observable } from "rxjs";
import { LoginService } from "../Services/login.service";

@Injectable({ providedIn: "root" })
export class AuthGuard implements CanActivate {
	constructor(private router: Router, private loginService: LoginService) {}
	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		const currentUser = this.loginService.currentUserValue;
		if (currentUser) {
			// El usuario esta logeado
			return true;
		}
		// No logeado, por lo tanto es redirigido a la pagina de login
		this.router.navigate(["/login"], { queryParams: { returnUrl: state.url } });
		return false;
	}
}
