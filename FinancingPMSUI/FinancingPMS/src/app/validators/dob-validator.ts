import { Validator, NG_VALIDATORS, AbstractControl } from '@angular/forms';
import { Directive } from '@angular/core';

@Directive({
    selector:'[dobValidator]',
    providers:[{
        provide:NG_VALIDATORS,
        useExisting:DOBValidator,
        multi:true
    }]
})
export class DOBValidator implements Validator{
    validate(control: AbstractControl): {[key:string]:any} | null {
        console.log(control);
        let date = new Date(control.value);
        if(date != null && date != undefined)
        {
            if(date.getFullYear() < 2000)
            {
                return null;
            }
        }
        return {'invaliddob' : true };
    }
}