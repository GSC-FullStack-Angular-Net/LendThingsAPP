export default class PersonForPartialUpdateDTO {
	constructor(
		public Id?: number,
		public Name?: string,
		public PhoneNumber?: string,
		public Email?: string
	) {}
}
