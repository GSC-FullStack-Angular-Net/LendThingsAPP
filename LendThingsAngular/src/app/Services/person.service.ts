import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import PersonBaseDTO from "../Models/PersonBaseDTO";
import PersonForCreationDTO from "../Models/PersonForCreationDTO";
import PersonForPartialUpdateDTO from "../Models/PersonForPartialUpdateDTO";

@Injectable({
	providedIn: "root",
})
export class PersonService {
	apiUrl = environment.apiUrl;
	constructor(private http: HttpClient) {}

	getAll() {
		return this.http.get<PersonBaseDTO[]>(`${this.apiUrl}person`);
	}
	getOne(id: number) {
		return this.http.get<PersonBaseDTO>(`${this.apiUrl}person/${id}`);
	}
	create(body: PersonForCreationDTO) {
		return this.http.post<PersonBaseDTO>(`${this.apiUrl}person`, body);
	}
	update(body: PersonForPartialUpdateDTO) {
		return this.http.patch<PersonBaseDTO>(`${this.apiUrl}person`, body);
	}
	delete(id: number) {
		return this.http.delete<string>(`${this.apiUrl}person${id}`);
	}
}
