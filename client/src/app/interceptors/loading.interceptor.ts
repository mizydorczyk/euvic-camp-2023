import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {delay, finalize, identity, Observable} from 'rxjs';
import {environment} from "../../environments/environment";
import {BusyService} from "../services/busy.service";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private busyService: BusyService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.busyService.busy();

    return next.handle(request).pipe(
      (environment.production ? identity : delay(1000)),
      finalize(() => {
        this.busyService.idle()
      })
    )
  }
}
