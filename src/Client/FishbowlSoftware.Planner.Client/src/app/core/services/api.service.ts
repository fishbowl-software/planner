import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, catchError, of} from 'rxjs';
import {MessageService} from 'primeng/api';
import {APP_CONFIG} from '@configs';
import {SearchableQuery} from '@core/models';

@Injectable()
export class ApiService {
  private baseUrl = APP_CONFIG.apiUrl;
  private headers: Record<string, string> = {'content-type': 'application/json'};

  constructor(
    private readonly httpClient: HttpClient,
    private readonly messageService: MessageService)
  {
  }

  private get<TResponse>(endpoint: string): Observable<TResponse> {
    return this.httpClient
      .get<TResponse>(this.baseUrl + endpoint)
      .pipe(catchError((err) => this.handleError(err)));
  }

  private post<TResponse, TBody>(endpoint: string, body: TBody): Observable<TResponse> {
    const bodyJson = JSON.stringify(body);

    return this.httpClient
      .post<TResponse>(this.baseUrl + endpoint, bodyJson, {headers: this.headers})
      .pipe(catchError((err) => this.handleError(err)));
  }

  private put<TResponse, TBody>(endpoint: string, body: TBody): Observable<TResponse> {
    const bodyJson = JSON.stringify(body);

    return this.httpClient
      .put<TResponse>(this.baseUrl + endpoint, bodyJson, {headers: this.headers})
      .pipe(catchError((err) => this.handleError(err)));
  }

  private delete<TResponse>(endpoint: string): Observable<TResponse> {
    return this.httpClient
      .delete<TResponse>(this.baseUrl + endpoint)
      .pipe(catchError((err) => this.handleError(err)));
  }

  private stringfySearchableQuery(query?: SearchableQuery): string {
    const search = query?.search ?? '';
    const orderBy = query?.orderBy ?? '';
    const page = query?.page ?? 1;
    const pageSize = query?.pageSize ?? 10;
    return `search=${search}&orderBy=${orderBy}&page=${page}&pageSize=${pageSize}`;
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private handleError(responseData: any): Observable<any> {
    const errorMessage = responseData.error?.error ?? responseData.error;

    this.messageService.add({key: 'notification', severity: 'error', summary: 'Error', detail: errorMessage});
    // this.messageService.add({key: 'errorMsg', severity: 'error', summary: 'Error', detail: errorMessage});
    console.error(errorMessage ?? responseData);
    return of({error: errorMessage, success: false});
  }
}
