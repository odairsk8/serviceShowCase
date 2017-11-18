
import { Injectable, NgZone } from '@angular/core';
import { ToastyService } from 'ng2-toasty';


@Injectable()
export class UserMessageService {

    constructor(private toastyService: ToastyService,
                private ngZone: NgZone) { }

    success(message: string) {
        this.ngZone.run(() => {
            this.toastyService.success({
                title: 'Sucess',
                msg: message || 'Saved with sucess',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
        });   
    }

    error(error: any){
        this.ngZone.run(() => {
            //TODO: show proper error
            this.toastyService.error({
                title: 'Sucess',
                msg: 'Something went wrong',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
        });
    }

}