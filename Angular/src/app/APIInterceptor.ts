import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import {Observable, catchError, map, throwError} from 'rxjs';
import { environment } from '../environments/environment';
import swal from 'sweetalert2';

@Injectable()
export class APIInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const apiReq = req.clone({ url: `${environment.baseUrl}${req.url}` });
    return next.handle(apiReq).pipe(
      map(res => {
      return res
      }),
      catchError((error: HttpErrorResponse) => {
          let errorMsg = '';
          if (error.error instanceof ErrorEvent) {
              errorMsg = `Error: ${error.error.message}`;
          } else {
              errorMsg = `Error Code: ${error.status},  Message: ${error.message}`;
          }
          swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: errorMsg
          });
          return throwError(errorMsg);
      })
  );

  }
}