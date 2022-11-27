import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatButtonModule } from "@angular/material/button";
import { MatTableModule } from "@angular/material/table";
import { MatIconModule } from "@angular/material/icon";
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { LoginComponent } from "./Components/login/login.component";
import { ErrorInterceptor } from "./Interceptors/error.interceptor";
import { JwtInterceptor } from "./Interceptors/jwt.interceptor";
import { HomeComponent } from "./Components/home/home.component";
import { CrudPersonComponent } from "./Components/crud-person/crud-person.component";
import { HeaderComponent } from "./Components/header/header.component";
import { EditFormComponent } from "./Components/crud-person/edit-form/edit-form.component";
import { CreateFormComponent } from "./Components/crud-person/create-form/create-form.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { PersonFormComponent } from "./Components/crud-person/person-form/person-form.component";
import { EliminateFormComponent } from "./Components/crud-person/eliminate-form/eliminate-form.component";

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		HomeComponent,
		CrudPersonComponent,
		HeaderComponent,
		EditFormComponent,
		CreateFormComponent,
		PersonFormComponent,
		EliminateFormComponent,
	],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		AppRoutingModule,
		ReactiveFormsModule,
		HttpClientModule,
		RouterModule,
		MatToolbarModule,
		MatButtonModule,
		MatTableModule,
		MatIconModule,
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		MatProgressSpinnerModule,
	],
	entryComponents: [
		CreateFormComponent,
		EditFormComponent,
		EliminateFormComponent,
	],
	providers: [
		{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
