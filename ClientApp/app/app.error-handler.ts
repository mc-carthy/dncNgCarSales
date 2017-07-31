import { ErrorHandler, Inject, NgZone } from "@angular/core";
import { ToastyService } from 'ng2-toasty';

import * as Raven from 'raven-js';

export class AppErrorHandler implements ErrorHandler {

    constructor(
        private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService
    ){}

    handleError(error: any) : void {
        Raven.captureException(error.originalError || error);
        this.ngZone.run(() => {
            this.toastyService.error({
                title: 'Error',
                msg: 'An unexpected error occured',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
        })
    }

}