import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filtroCliente',
  standalone:false
})
export class FiltroClientePipe implements PipeTransform {
  transform(clientes: any[], termo: string): any[] {
    if (!clientes || !termo) return clientes;
    const termoLower = termo.toLowerCase();
    return clientes.filter(c =>
      c.nome.toLowerCase().includes(termoLower) ||
      c.email.toLowerCase().includes(termoLower)
    );
  }
}