import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {environment} from "../../environments/environment";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          if (error.status === 400) {
            this.toastr.error(error.error)
          }
          if (error.status === 401) {
            if (request.url !== environment.apiUrl + 'account') {
              this.toastr.error(error.error);
            }
          }
          if (error.status === 404) {
            this.toastr.error('Thing you are looking for isn\'t there');
            this.router.navigateByUrl('/ranking');
          }
          if (error.status === 500) {
            this.toastr.error('Internal server error. Something went wrong');
            this.router.navigateByUrl('/ranking');
          }
        }
        return throwError(() => new Error(error.message));
      })
    )
  }
}
