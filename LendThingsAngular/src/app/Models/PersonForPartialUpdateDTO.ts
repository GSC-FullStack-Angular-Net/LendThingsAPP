export default class PersonForPartialUpdateDTO {
	constructor(
		public id: number,
		public name?: string,
		public phoneNumber?: string,
		public email?: string
	) {}
}
