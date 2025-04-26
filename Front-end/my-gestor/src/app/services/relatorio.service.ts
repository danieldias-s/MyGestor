import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';

@Injectable({ providedIn: 'root' })
export class RelatorioService {
   private apiUrl = `${environment.apiUrl}/relatorio`;

  constructor(private http: HttpClient) {}

  getRelatorio(ano: number, mes: number): Observable<any[]> {
    const params = new HttpParams()
      .set('ano', ano)
      .set('mes', mes);
    return this.http.get<any[]>(this.apiUrl, { params });
  }
}
