import { HttpErrorResponse } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { first } from "rxjs";
import PersonBaseDTO from "src/app/Models/PersonBaseDTO";
import { PersonService } from "src/app/Services/person.service";
import { CreateFormComponent } from "./create-form/create-form.component";

import { EditFormComponent } from "./edit-form/edit-form.component";
import { EliminateFormComponent } from "./eliminate-form/eliminate-form.component";

@Component({
	selector: "app-crud-person",
	templateUrl: "./crud-person.component.html",
	styleUrls: ["./crud-person.component.css"],
})
export class CrudPersonComponent implements OnInit {
	columnsToDisplay = ["Id", "Name", "PhoneNumber", "Email", "operations"];
	listPeople: PersonBaseDTO[] = [];
	constructor(
		private personService: PersonService,
		private dialog: MatDialog
	) {}

	ngOnInit(): void {
		this.updatePeopleList();
	}
	updatePeopleList() {
		this.personService
			.getAll()
			.pipe(first())
			.subscribe(
				(data) => {
					this.listPeople = data as PersonBaseDTO[];
				},
				(error: HttpErrorResponse) => {
					alert("Error while retriving data from server, please try again.");
					throw error;
				}
			);
	}

	editPerson(person: PersonBaseDTO) {
		const dialogConfig = new MatDialogConfig();
		dialogConfig.disableClose = true;
		dialogConfig.autoFocus = true;
		dialogConfig.data = person;
		const dialogRef = this.dialog.open(EditFormComponent, dialogConfig);
		dialogRef.afterClosed().subscribe(() => {
			this.updatePeopleList();
		});
	}
	deletePerson(person: PersonBaseDTO) {
		const dialogConfig = new MatDialogConfig();
		dialogConfig.disableClose = true;
		dialogConfig.autoFocus = true;
		dialogConfig.data = person;
		const dialogRef = this.dialog.open(EliminateFormComponent, dialogConfig);
		dialogRef.afterClosed().subscribe(() => {
			this.updatePeopleList();
		});
	}
	createPerson() {
		const dialogConfig = new MatDialogConfig();
		dialogConfig.disableClose = true;
		dialogConfig.autoFocus = true;
		const dialogRef = this.dialog.open(CreateFormComponent, dialogConfig);
		dialogRef.afterClosed().subscribe(() => {
			this.updatePeopleList();
		});
	}
}
