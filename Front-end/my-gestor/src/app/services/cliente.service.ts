import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Cliente } from '../models/cliente';
import { environment } from '../environment/environment';
import { catchError, map,tap } from 'rxjs/operators';




@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private apiUrl = `${environment.apiUrl}/clientes`;

  constructor(private http: HttpClient) {}


listarCliente(): Observable<Cliente[]> {
  console.log('Fazendo requisição para:', this.apiUrl);
  return this.http.get<Cliente[]>(this.apiUrl).pipe(
    tap({
      next: res => console.log('Resposta recebida:', res),
      error: err => console.error('Erro na requisição:', err),
      complete: () => console.log('Requisição completada')
    }),
    catchError(error => {
      console.error('Erro capturado:', error);
      return of([]);
    })
  );
}
  
  adicionarCLiente(cliente: Cliente): Observable<Cliente> {
    return this.http.post<Cliente>(this.apiUrl, cliente);
  }

  atualizarCliente(id: number, cliente: Cliente): Observable<Cliente> {
    return this.http.put<Cliente>(`${this.apiUrl}/${id}`, cliente);
  }

  deletarCliente(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getClienteById(id: number): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.apiUrl}/${id}`);
  }
}
