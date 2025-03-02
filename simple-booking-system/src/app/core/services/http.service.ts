import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResourceDto } from '../../interfaces/resource.interface';
import { Observable } from 'rxjs';
import { IBookResourceRequest } from '../../interfaces/book-resource-request.interface';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private apiUrl: string = 'http://localhost:5173/api/resources';

  constructor(private readonly httpClient: HttpClient) {}

  public getAllResources(): Observable<IResourceDto[]> {
    return this.httpClient.get<IResourceDto[]>(`${this.apiUrl}/all`);
  }

  public bookResource(request: IBookResourceRequest): Observable<void> {
    return this.httpClient.post<void>(`${this.apiUrl}/book-resource`, request);
  }
}
