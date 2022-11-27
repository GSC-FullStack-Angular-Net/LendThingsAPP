import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CrudPersonComponent } from "./Components/crud-person/crud-person.component";
import { HomeComponent } from "./Components/home/home.component";
import { LoginComponent } from "./Components/login/login.component";
import { AuthGuard } from "./Guards/auth.guard";

const routes: Routes = [
	{
		path: "",
		component: HomeComponent,
		canActivate: [AuthGuard],
		pathMatch: "full",
	},
	{ path: "login", component: LoginComponent },
	{ path: "person", component: CrudPersonComponent, canActivate: [AuthGuard] },
	{ path: "**", redirectTo: "login" },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
