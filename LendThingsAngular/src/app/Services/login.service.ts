import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { User } from "../Models/User";
import { BehaviorSubject, map, Observable } from "rxjs";
import { environment } from "src/environments/environment";
import jwt_decode from "jwt-decode";
import { DecodedToken } from "../Models/DecodedToken";

@Injectable({ providedIn: "root" })
export class LoginService {
	private currentUserSubject: BehaviorSubject<User | null>;
	public currentUser: Observable<User | null>;

	constructor(private http: HttpClient) {
		this.currentUserSubject = new BehaviorSubject<User | null>(
			localStorage.getItem("currentUser") != null
				? JSON.parse(localStorage.getItem("currentUser") ?? "")
				: null
		);
		this.currentUser = this.currentUserSubject.asObservable();
	}
	public get currentUserValue(): User | null {
		return this.currentUserSubject.value;
	}

	login(username: string, password: string) {
		return this.http
			.post<any>(`${environment.apiUrl}/login/login`, {
				username,
				password,
			})
			.pipe(
				map((serializedToken) => {
					try {
						var token: string = jwt_decode(serializedToken["AccessToken"]);
						var decodedToken = new DecodedToken(token);
						var splittedFullName = decodedToken.fullName.split(" ");
						const user = new User(
							decodedToken.userName,
							splittedFullName[0],
							splittedFullName[splittedFullName.length - 1],
							serializedToken["AccessToken"]
						);
						// Almacena los detalles del usuario y el token JWT para mantener
						// al usuario logeado incluso entre actualizaciones de las páginas
						localStorage.setItem("currentUser", JSON.stringify(user));
						this.currentUserSubject.next(user);
						return user;
					} catch (error) {
						throw error;
					}
				})
			);
	}
	logout() {
		// Elimina al usuario del localStorage para cerrar sesión
		localStorage.removeItem("currentUser");
		this.currentUserSubject.next(null);
	}
}
