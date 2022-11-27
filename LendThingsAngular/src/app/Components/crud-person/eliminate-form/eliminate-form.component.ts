import { HttpErrorResponse } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { throws } from "assert";
import PersonBaseDTO from "src/app/Models/PersonBaseDTO";
import { PersonService } from "src/app/Services/person.service";
import { EditFormComponent } from "../edit-form/edit-form.component";

@Component({
	selector: "app-eliminate-form",
	templateUrl: "./eliminate-form.component.html",
	styleUrls: ["./eliminate-form.component.css"],
})
export class EliminateFormComponent implements OnInit {
	errorWhileEliminating: any;
	constructor(
		private personService: PersonService,
		public dialogRef: MatDialogRef<EditFormComponent>,
		@Inject(MAT_DIALOG_DATA) public currentPerson: any,
		private formBuilder: FormBuilder
	) {}
	personForm!: FormGroup<any>;

	ngOnInit(): void {
		this.personForm = this.formBuilder.group({
			name: [this.currentPerson?.Name ?? ""],
			phoneNumber: [this.currentPerson?.PhoneNumber ?? ""],
			email: [this.currentPerson?.Email ?? ""],
		});
		this.personForm.disable();
	}

	confirm() {
		if (
			this.personForm.invalid &&
			!this.personForm.controls["email"].hasError("emailValidation")
		) {
			return;
		}

		this.personService.delete(this.currentPerson.Id).subscribe(
			(response) => {
				alert(response.msg);
				this.dialogRef.close();
			},
			(error: HttpErrorResponse) => {
				alert("Something has happened while eliminating, please try again.");
			}
		);
	}
}
