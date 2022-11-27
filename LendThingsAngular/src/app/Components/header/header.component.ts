import { Component, OnInit } from "@angular/core";
import { User } from "src/app/Models/User";
import { LoginService } from "src/app/Services/login.service";

@Component({
	selector: "app-header",
	templateUrl: "./header.component.html",
	styleUrls: ["./header.component.css"],
})
export class HeaderComponent implements OnInit {
	currentUser!: User;

	constructor(private loginService: LoginService) {}

	ngOnInit(): void {
		this.currentUser = this.loginService.currentUserValue as User;
	}
}
