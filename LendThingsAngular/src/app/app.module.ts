import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { LoginComponent } from "./Components/login/login.component";
import { ReactiveFormsModule } from "@angular/forms";
import { ErrorInterceptor } from "./Interceptors/error.interceptor";
import { JwtInterceptor } from "./Interceptors/jwt.interceptor";
import { HomeComponent } from "./Components/home/home.component";
import { CrudPersonComponent } from "./Components/crud-person/crud-person.component";

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		HomeComponent,
		CrudPersonComponent,
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		ReactiveFormsModule,
		HttpClientModule,
	],
	providers: [ErrorInterceptor, JwtInterceptor],
	bootstrap: [AppComponent],
})
export class AppModule {}
