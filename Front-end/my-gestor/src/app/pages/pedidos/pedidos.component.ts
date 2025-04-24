import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PedidoService } from '../../services/pedido.service';
import { Pedido } from '../../models/pedidos';

@Component({
  selector: 'app-pedidos',
  standalone: false,
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css']
})
export class PedidosComponent implements OnInit {
  pedidos: Pedido[] = [];
  form!: FormGroup;
  editando: boolean = false;

  constructor(private pedidoService: PedidoService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.carregarPedidos();
    this.inicializarFormulario();
  }

  inicializarFormulario() {
    this.form = this.fb.group({
      id: [0],
      clienteId: [''],
      clienteNome: [''],
      dataPedido: [''],
      total: [0],
      itens: this.fb.array([])
    });
  }

  carregarPedidos(): void {
    this.pedidoService.listar().subscribe({
      next: (pedidos: any[]) => {
     
        this.pedidos = pedidos.map((p: any) => ({
          id: Number(p.id) || 0,
          clienteId: Number(p.clienteId) || 0,
          clienteNome: String(p.cliente || ''),
          dataPedido: this.converterParaData(p.dataPedido) || null,
          total: this.converterParaNumero(p.total),
          itens: Array.isArray(p.itens) ? p.itens : []
        }));
      },
      error: (err) => console.error('Erro ao carregar pedidos:', err)
    });
  }

  private converterParaData(data: any): Date | null {
    if (!data) return null;
    if (data instanceof Date) return data;
  
 
    const partes = String(data).split('/');
    if (partes.length === 3) {
      const [dia, mes, ano] = partes.map(Number);
      return new Date(ano, mes - 1, dia); 
    }
  
    const parsedDate = new Date(data);
    return isNaN(parsedDate.getTime()) ? null : parsedDate;
  }
  private converterParaNumero(valor: any): number {
    if (typeof valor === 'string') {
      return Number(
        valor.replace(/[^\d,.-]/g, '').replace(',', '.')
      );
    }
    return typeof valor === 'number' ? valor : 0;
  }

  salvar() {
    const pedido: Pedido = this.form.value;

    if (this.editando) {
      this.pedidoService.atualizar(pedido).subscribe({
        next: () => {
          this.carregarPedidos();
          this.editando = false;
          this.form.reset();
        },
        error: (err) => console.error('Erro ao atualizar pedido', err)
      });
    } else {
      this.pedidoService.criar(pedido).subscribe({
        next: () => {
          this.carregarPedidos();
          this.form.reset();
        },
        error: (err) => console.error('Erro ao criar pedido', err)
      });
    }
  }

  editar(pedido: Pedido) {
    this.editando = true;
    this.form.patchValue({
      ...pedido,
      dataPedido: pedido.dataPedido?.toISOString().substring(0, 10)
    });
  }

  excluir(id: number) {
    if (confirm('Tem certeza que deseja excluir este pedido?')) {
      this.pedidoService.deletar(id).subscribe({
        next: () => this.carregarPedidos(),
        error: (err) => console.error('Erro ao excluir pedido', err)
      });
    }
  }

  cancelar() {
    this.editando = false;
    this.form.reset();
  }
}
