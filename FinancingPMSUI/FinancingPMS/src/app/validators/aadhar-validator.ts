import { Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { Directive } from '@angular/core';

@Directive({
    selector:'[aadhaarValidator]',
    providers:[{
        provide:NG_VALIDATORS,
        useExisting:AadhaarValidator,
        multi:true
    }]
})
export class AadhaarValidator implements Validator{
    validate(control: AbstractControl): {[key:string]:any} | null {
        // console.log(control);
        let aadhaarPatternRegex = /^[0-9]{12}$/;

        if(aadhaarPatternRegex.test(control.value))
        {
            return null;
        }
        return {'doNotMatch':true};
    }
}