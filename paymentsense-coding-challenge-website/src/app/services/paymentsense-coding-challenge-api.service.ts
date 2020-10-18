import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Country } from '../model/country';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {
  constructor(private httpClient: HttpClient) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get('https://localhost:44380/health', { responseType: 'text' });
  }

  public getCountries(): Observable<any> {
    return this.httpClient.get('https://localhost:44380/api/countries', httpOptions);
  }
}
