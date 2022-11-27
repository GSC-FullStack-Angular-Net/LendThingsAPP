import { Component, Input, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";

@Component({
	selector: "app-person-form",
	templateUrl: "./person-form.component.html",
	styleUrls: ["./person-form.component.css"],
})
export class PersonFormComponent implements OnInit {
	@Input() form!: FormGroup<any>;
	@Input() nameError!: string;
	@Input() phoneNumberError!: string;
	@Input() emailError!: string;

	constructor() {}

	ngOnInit(): void {}
}
