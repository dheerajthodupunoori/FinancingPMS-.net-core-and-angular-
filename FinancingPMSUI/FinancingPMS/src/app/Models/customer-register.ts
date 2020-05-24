export class RegisterCustomer{
    public CustomerRegistrationValidationStatus : number;
    public AadhaarImage : File;
    constructor(
        public FirstName:string,
        public LastName:string,
        public FatherName:string,
        public DOB:Date,
        public Password:string,
        public AadhaarNumber:string,
        public FirmID:string
    )
    {
        
    }
}