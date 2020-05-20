export class RegisterCustomer{
    constructor(
        public FirstName:string,
        public LastName:string,
        public FatherName:string,
        public DOB:Date,
        public Password:string,
        public Aadhar:string,
        public FirmID:string,
        public CnfPwd:string
    )
    {
        
    }
}