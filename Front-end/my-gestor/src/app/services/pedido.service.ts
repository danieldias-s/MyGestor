import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Pedido } from '../models/pedidos';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  private readonly apiUrl = 'http://localhost:5221/api/pedidos';

  constructor(private http: HttpClient) {}

  listar(): Observable<Pedido[]> {
    return this.http.get<Pedido[]>(this.apiUrl).pipe(
      map(pedidos =>
        pedidos.map(pedido => ({
          ...pedido,
        
          dataPedido: pedido.dataPedido ? pedido.dataPedido : null,
       
          total: pedido.total || (
            pedido.itens?.reduce((sum, item) =>
              sum + (item.precoUnitario * item.quantidade), 0
            ) || 0
          )
        }))
      )
    );
  }
  

  obterPorId(id: number): Observable<Pedido> {
    return this.http.get<Pedido>(`${this.apiUrl}/${id}`);
  }

  criar(pedido: Pedido): Observable<Pedido> {
   
    pedido.dataPedido = new Date(); 
    return this.http.post<Pedido>(this.apiUrl, pedido);
  }

  atualizar(pedido: Pedido): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${pedido.id}`, pedido);
  }

  deletar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}