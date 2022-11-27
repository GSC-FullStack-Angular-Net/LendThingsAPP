export class DecodedToken {
	public aud: string;
	public exp: number;
	public roles: string[];
	public fullName: string;
	public userName: string;
	public issuer: string;

	constructor(decodedToken: any) {
		this.aud = decodedToken["aud"];
		this.exp = decodedToken["exp"];
		this.roles =
			decodedToken[
				"http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
			];
		this.fullName =
			decodedToken[
				"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"
			];
		this.userName =
			decodedToken[
				"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
			];
		this.issuer = decodedToken.iss;
	}
}
