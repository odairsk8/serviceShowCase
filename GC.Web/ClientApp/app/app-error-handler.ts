
//import * as Raven from 'raven-js';
import { Inject, ErrorHandler, NgZone, isDevMode } from "@angular/core";
import { UserMessageService } from "./components/shared/user-message.service";

export class AppErrorHandler implements ErrorHandler {

    constructor( @Inject(NgZone) private ngZone: NgZone,
                @Inject(UserMessageService) private userMessageService: UserMessageService) {

    }

    handleError(error: any): void {

        this.ngZone.run(() => {
            if (typeof (window) !== 'undefined') {
                console.log(error.json());
                this.userMessageService.error(error);
            }
        });

        if (!isDevMode())
            throw error;
        //Raven.captureException(error.originalError || error);
        else
            throw error;


    }
}