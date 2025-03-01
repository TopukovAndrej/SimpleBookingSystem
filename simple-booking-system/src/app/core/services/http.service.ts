import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResourceDto } from '../../shared/interfaces/resource.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private apiUrl: string = 'http://localhost:5173/api/resources';

  constructor(private readonly httpClient: HttpClient) {}

  public getAllResources(): Observable<IResourceDto[]> {
    return this.httpClient.get<IResourceDto[]>(`${this.apiUrl}/all`);
  }
}
