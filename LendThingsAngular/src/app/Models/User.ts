export class User {
	constructor(
		public username: string,
		public firstName: string,
		public lastName: string,
		public token?: string
	) {}
}
