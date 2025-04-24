import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RelatorioService {
  private readonly apiUrl = 'http://localhost:5221/api/Pedidos/relatorio'; // ajuste conforme necessário

  constructor(private http: HttpClient) {}

  getRelatorio(ano: number, mes: number): Observable<any[]> {
    const params = new HttpParams()
      .set('ano', ano)
      .set('mes', mes);
    return this.http.get<any[]>(this.apiUrl, { params });
  }
}
