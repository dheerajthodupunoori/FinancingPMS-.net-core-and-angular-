import { Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { Directive } from '@angular/core';

@Directive({
    selector:'[emailValidator]',
    providers:[{
        provide:NG_VALIDATORS,
        useExisting:EmailValidator,
        multi:true
    }]
})
export class EmailValidator implements Validator{
    validate(control: AbstractControl): {[key:string]:any} | null {
        console.log(control);
        let emailPatternRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

        if(emailPatternRegex.test(control.value))
        {
            console.log("valid");
            return null;
        }
        return {'notValid':true};
    }
}