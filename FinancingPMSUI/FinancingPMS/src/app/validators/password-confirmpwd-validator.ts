import { Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { Directive, Input } from '@angular/core';


@Directive({
    selector:'[passwordValidator]',
    providers:[{
        provide:NG_VALIDATORS,
        useExisting:ConfirmPassword,
        multi:true
    }]
})
export class ConfirmPassword implements Validator{
    @Input() passwordValidator : string;
    validate(control: AbstractControl): {[Key:string]:any} | null {
        console.log(control);
        const controlToCompare =  control.parent.get(this.passwordValidator);
        if(controlToCompare && controlToCompare.value!==control.value)
        {
            return {'notEqual':true};
        }
        return null;
    }
    
}