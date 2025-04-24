import { Injectable } from '@angular/core';
import { Observable, catchError, forkJoin, map, of, timeout } from 'rxjs';
import { ClienteService } from './cliente.service';
import { ProdutosService } from './produto.service';
import { UsuarioService } from './usuario.service';
import { PedidoService } from './pedido.service';
import { DashboardCounts } from '../models/DashboardCounts';
import { Pedido } from '../models/pedidos';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  constructor(
    private clienteService: ClienteService,
    private produtoService: ProdutosService,
    private pedidoService: PedidoService,
    private usuarioService: UsuarioService
  ) {}

  getClientesCount(): Observable<number> {
    return this.clienteService.listarCliente().pipe(
      map(clientes => clientes.length),
      catchError(() => of(0))
    );
  }

  getProdutosCount(): Observable<number> {
    return this.produtoService.listar().pipe(
      map(produtos => produtos.length),
      catchError(() => of(0))
    );
  }

  getPedidosCount(): Observable<number> {
    return this.pedidoService.listar().pipe(
      map(pedidos => pedidos.length),
      catchError(() => of(0))
    );
  }

  getUsuariosCount(): Observable<number> {
    return this.usuarioService.listar().pipe(
      map(usuarios => usuarios.length),
      catchError(() => of(0))
    );
  }

  getTotalVendas(): Observable<number> {
    return this.pedidoService.listar().pipe(
      map((pedidos: Pedido[]) => {
       
        return pedidos.reduce((total, pedido) => {
          if (pedido.total) {
            return total + pedido.total;
          } else if (pedido.itens && pedido.itens.length > 0) {
            
            const pedidoTotal = pedido.itens.reduce((sum, item) => 
              sum + (item.precoUnitario * item.quantidade), 0);
            return total + pedidoTotal;
          }
          return total;
        }, 0);
      }),
      catchError(() => of(0))
    );
  }

  getDashboardCounts(): Observable<DashboardCounts> {
    return forkJoin({
      clientes: this.getClientesCount(),
      produtos: this.getProdutosCount(),
      pedidos: this.getPedidosCount(),
      usuarios: this.getUsuariosCount(),
      totalVendas: this.getTotalVendas()
    });
  }

  getPedidosPorMes(): Observable<{ meses: string[], valores: number[] }> {
    return this.pedidoService.listar().pipe(
      map(pedidos => {
        const contagemMeses: { [mes: string]: number } = {};

        pedidos.forEach(p => {
          if (p.dataPedido) {
            const data = new Date(p.dataPedido);
            const mesAno = data.toLocaleDateString('pt-BR', { month: 'short', year: 'numeric' });
            contagemMeses[mesAno] = (contagemMeses[mesAno] || 0) + 1;
          }
        });

        const mesesOrdenados = Object.keys(contagemMeses).sort((a, b) => {
          const [m1, y1] = a.split(' ');
          const [m2, y2] = b.split(' ');
          return new Date(`${y1}-${m1}-01`).getTime() - new Date(`${y2}-${m2}-01`).getTime();
        });

        return { 
          meses: mesesOrdenados, 
          valores: mesesOrdenados.map(mes => contagemMeses[mes]) 
        };
      }),
      catchError(() => of({ meses: [], valores: [] }))
    );
  }

  getPedidosRecentes(): Observable<any[]> {
    return this.pedidoService.listar().pipe(
      map(pedidos => pedidos
        .sort((a, b) => {
          const dataA = a.dataPedido ? new Date(a.dataPedido).getTime() : 0;
          const dataB = b.dataPedido ? new Date(b.dataPedido).getTime() : 0;
          return dataB - dataA;
        })
        .slice(0, 5)
        .map(p => ({
          ...p,
          dataFormatada: p.dataPedido ? new Date(p.dataPedido).toLocaleDateString('pt-BR') : 'Data inválida'
        }))
      ),
      catchError(() => of([]))
    );
  }
}