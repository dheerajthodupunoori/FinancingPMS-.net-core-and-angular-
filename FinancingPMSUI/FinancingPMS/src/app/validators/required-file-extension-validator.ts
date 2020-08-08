import { FormControl, AbstractControl, ValidatorFn } from '@angular/forms';

export function requiredFileType( type: string ) : ValidatorFn {
    return (control: AbstractControl) : {[key:string] : boolean} | null => {
      const file = control.value;
    //   console.log(control);
      if ( file ) {
        const extension = file.name.split('.')[1].toLowerCase();
        if ( type.toLowerCase() !== extension.toLowerCase() ) {
          return {
            'requiredFileType': true
          };
        }
      }
  
      return null;
    };
  }