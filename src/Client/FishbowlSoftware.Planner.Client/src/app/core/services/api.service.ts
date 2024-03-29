import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, catchError, of} from 'rxjs';
import {APP_CONFIG} from '@configs';
import {ToastService} from '@core/services';
import {
  SearchableQuery,
  PagedResult,
  UserDto,
  ClientDto,
  CreateClientCommand,
  UpdateClientCommand,
  Result,
  ProjectDto,
  CreateProjectCommand,
  UpdateProjectCommand,
  ApplicationDto,
  CreateApplicationCommand,
  UpdateApplicationCommand,
} from '@core/models';

@Injectable({providedIn: 'root'})
export class ApiService {
  private readonly baseUrl = APP_CONFIG.apiUrl;
  private readonly headers: Record<string, string> = {'content-type': 'application/json'};

  constructor(
    private readonly httpClient: HttpClient,
    private readonly toastService: ToastService,
  )
  {
  }

  //#region Users API

  getUsers(query?: SearchableQuery): Observable<PagedResult<UserDto>> {
    const url = `/users?${this.stringfySearchableQuery(query)}`;
    return this.get(url);
  }

  //#endregion


  //#region Clients API

  getClient(id: string): Observable<Result<ClientDto>> {
    const url = `/clients/${id}`;
    return this.get(url);
  }

  getClients(query?: SearchableQuery): Observable<PagedResult<ClientDto>> {
    const url = `/clients?${this.stringfySearchableQuery(query)}`;
    return this.get(url);
  }

  createClient(command: CreateClientCommand): Observable<Result> {
    const url = `/clients`;
    return this.post(url, command);
  }

  updateClient(command: UpdateClientCommand): Observable<Result> {
    const url = `/clients/${command.id}`;
    return this.put(url, command);
  }

  deleteClient(id: string): Observable<Result> {
    const url = `/clients/${id}`;
    return this.delete(url);
  }

  //#endregion


  //#region Projects API

  getProject(id: string): Observable<Result<ProjectDto>> {
    const url = `/projects/${id}`;
    return this.get(url);
  }

  getProjects(query?: SearchableQuery): Observable<PagedResult<ProjectDto>> {
    const url = `/projects?${this.stringfySearchableQuery(query)}`;
    return this.get(url);
  }

  createProject(command: CreateProjectCommand): Observable<Result> {
    const url = `/projects`;
    return this.post(url, command);
  }

  updateProject(command: UpdateProjectCommand): Observable<Result> {
    const url = `/projects/${command.id}`;
    return this.put(url, command);
  }

  deleteProject(clientId: string): Observable<Result> {
    const url = `/projects/${clientId}`;
    return this.delete(url);
  }

  //#endregion


  //#region Applications API

  getApplication(id: string): Observable<Result<ApplicationDto>> {
    const url = `/applications/${id}`;
    return this.get(url);
  }

  getApplications(query?: SearchableQuery): Observable<PagedResult<ApplicationDto>> {
    const url = `/applications?${this.stringfySearchableQuery(query)}`;
    return this.get(url);
  }

  createApplication(command: CreateApplicationCommand): Observable<Result> {
    const url = `/applications`;
    return this.post(url, command);
  }

  updateApplication(command: UpdateApplicationCommand): Observable<Result> {
    const url = `/applications/${command.id}`;
    return this.put(url, command);
  }

  deleteApplication(clientId: string): Observable<Result> {
    const url = `/applications/${clientId}`;
    return this.delete(url);
  }

  //#endregion


  //#region Internal methods

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

    this.toastService.showError(errorMessage);
    console.error(errorMessage ?? responseData);
    return of({error: errorMessage, success: false});
  }

  //#endregion
}
