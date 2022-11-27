import { HttpErrorResponse } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import PersonForPartialUpdateDTO from "src/app/Models/PersonForPartialUpdateDTO";
import { PersonService } from "src/app/Services/person.service";

@Component({
	selector: "app-edit-form",
	templateUrl: "./edit-form.component.html",
	styleUrls: ["./edit-form.component.css"],
})
export class EditFormComponent implements OnInit {
	emailError!: string;
	phoneNumberError!: string;
	nameError!: string;
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
	}

	confirm() {
		if (
			this.personForm.invalid &&
			!this.personForm.controls["email"].hasError("emailValidation")
		) {
			return;
		}

		var formPerson = new PersonForPartialUpdateDTO(
			this.currentPerson.Id,
			this.personForm.controls["name"].value,
			this.personForm.controls["phoneNumber"].value,
			this.personForm.controls["email"].value
		);

		this.personService.update(formPerson).subscribe(
			(response) => {
				this.dialogRef.close();
			},
			(error: HttpErrorResponse) => {
				this.setErrors(error.error.errors);
			}
		);
	}

	setErrors(errors: {
		Email?: string[];
		Name?: string[];
		PhoneNumber?: string[];
	}) {
		if (errors.Email != null) {
			this.personForm.controls["email"].setErrors({ emailValidation: true });
			this.emailError = errors.Email[0];
		}
		if (errors.Name != null) {
			this.personForm.controls["name"].setErrors({ nameValidation: true });
			this.nameError = errors.Name[0];
		}
		if (errors.PhoneNumber != null) {
			this.personForm.controls["phoneNumber"].setErrors({
				phoneNumberValidation: true,
			});
			this.phoneNumberError = errors.PhoneNumber[0];
		}
	}
}
