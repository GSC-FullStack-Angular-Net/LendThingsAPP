import { HttpErrorResponse } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from "rxjs";
import { LoginService } from "src/app/Services/login.service";

@Component({
	selector: "app-login",
	templateUrl: "./login.component.html",
	styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
	loginForm!: FormGroup;
	loading = false;
	submitted = false;
	returnUrl!: string;
	error = "";

	constructor(
		private formBuilder: FormBuilder,
		private route: ActivatedRoute,
		private router: Router,
		private loginService: LoginService
	) {
		// redirige al Inicio si ya está logeado
		if (this.loginService.currentUserValue) {
			this.router.navigate(["/"]);
		}
	}

	ngOnInit() {
		this.loginForm = this.formBuilder.group({
			username: ["", Validators.required],
			password: ["", Validators.required],
		});
		// get return url from route parameters or default to '/'
		this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
	}

	// convenience getter for easy access to form fields
	get formFields() {
		return this.loginForm.controls;
	}
	onSubmit() {
		this.submitted = true; // stop here if form is invalid
		if (this.loginForm.invalid) {
			return;
		}
		this.loading = true;
		this.loginService
			.login(
				this.formFields["username"].value,
				this.formFields["password"].value
			)
			.pipe(first())
			.subscribe(
				(data) => {
					this.router.navigate([this.returnUrl]);
				},
				(error: HttpErrorResponse) => {
					this.error = this.errorHandling(error);
					this.loading = false;
				}
			);
	}
	errorHandling(response: HttpErrorResponse): string {
		switch (response.status) {
			case 403:
				return "We cannot find an account with that UserName/Password.";
			default:
				return "We are having inconvinients, try again later.";
		}
	}
}
