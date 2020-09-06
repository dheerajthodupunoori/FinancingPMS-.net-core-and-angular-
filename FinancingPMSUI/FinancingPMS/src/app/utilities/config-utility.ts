import { environment } from "../../environments/environment";
import { Firm, Login, Register, FileUpload } from "../Enums/OperationType";
import {
  FileUploadConfigLocal,
  FileUploadConfigServer,
} from "../../config/file-upload-config";
import { FirmConfigLocal, FirmConfigServer } from "../../config/firm-config";
import { LoginConfigLocal, LoginConfigServer } from "../../config/login-config";
import {
  RegisterConfigLocal,
  RegisterConfigServer,
} from "../../config/register-config";

export class ConfigUtility {
  static GetURL(operationType: any): string {
    if (environment.useLocalURL) {
      console.log(operationType);
      switch (operationType) {
        case FileUpload.AadhaarUpload:
          return FileUploadConfigLocal.aadhaarUploadForCustomerRegistrationURL;
        case Firm.GetAllFirms:
          return FirmConfigLocal.getAllFirms;
        case Login.FirmLogin:
          return LoginConfigLocal.firmloginURL;
        case Login.CustomerLogin:
          return LoginConfigLocal.customerLoginURL;
        case Register.FirmRegistration:
          return RegisterConfigLocal.firmRegistrationURL;
        case Register.SaveFirmAdditionalDetails:
          return RegisterConfigLocal.saveFirmAdditionalDetailsURL;
        case Register.CustomerRegistration:
          return RegisterConfigLocal.customerRegistrationToFirmURL;
        case Register.SaveCustomerAdditionalDetails:
          return RegisterConfigLocal.saveCustomerAdditinalDetailsURL;
        default:
          return "";
      }
    } else {
      switch (operationType) {
        case FileUpload.AadhaarUpload:
          return FileUploadConfigServer.aadhaarUploadForCustomerRegistrationURL;
        case Firm.GetAllFirms:
          return FirmConfigServer.getAllFirms;
        case Login.FirmLogin:
          return LoginConfigServer.firmloginURL;
        case Login.CustomerLogin:
          return LoginConfigServer.customerLoginURL;
        case Register.FirmRegistration:
          return RegisterConfigServer.firmRegistrationURL;
        case Register.SaveFirmAdditionalDetails:
          return RegisterConfigServer.saveFirmAdditionalDetailsURL;
        case Register.CustomerRegistration:
          return RegisterConfigServer.customerRegistrationToFirmURL;
        case Register.SaveCustomerAdditionalDetails:
          return RegisterConfigServer.saveCustomerAdditinalDetailsURL;
        default:
          return "";
      }
    }
  }
}
