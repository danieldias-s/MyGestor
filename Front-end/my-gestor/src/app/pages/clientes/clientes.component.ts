import { Component, OnInit } from '@angular/core';
import { Cliente } from '../../models/cliente';
import { ClienteService } from '../../services/cliente.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clientes',
  standalone:false,
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  clientes: Cliente[] = [];
  filtro: string = ''; 

  constructor(
    private clienteService: ClienteService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarClientes();
  }

  carregarClientes() {
    this.clienteService.listarCliente().subscribe({
      next: data => this.clientes = data,
      error: err => console.error('Erro ao carregar clientes', err)
    });
  }

  editarCliente(id: number): void {
    this.router.navigate(['/clientes/editar', id]);
  }

  deletarCliente(id: number): void {
    if (confirm('Tem certeza que deseja deletar este cliente?')) {
      this.clienteService.deletarCliente(id).subscribe(() => {
        this.carregarClientes();
      });
    }
  }

  novoCliente(): void {
    this.router.navigate(['/clientes/novo']);
  }
}