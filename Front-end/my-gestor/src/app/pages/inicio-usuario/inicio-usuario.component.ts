import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { PedidoService } from '../../services/pedido.service';
import { UsuarioService } from '../../services/usuario.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-inicio-usuario',
  standalone:false,
  templateUrl: './inicio-usuario.component.html',
  styleUrls: ['./inicio-usuario.component.css']
})
export class InicioUsuarioComponent implements OnInit {
  nomeUsuario = '';
  emailUsuario: string | null = '';
  totalPedidos = 0;
  ultimosPedidos: any[] = [];

  constructor(
    private authService: AuthService,
    private pedidoService: PedidoService,
    private usuarioService: UsuarioService
  ) {}

  ngOnInit(): void {
    this.emailUsuario = this.authService.getUserEmail();

    this.usuarioService.getUsuario().subscribe(usuario => {
      this.nomeUsuario = usuario?.nome || 'Usuário';

      
      this.pedidoService.listar().subscribe(pedidos => {
        this.ultimosPedidos = pedidos.slice(-3).reverse();
        this.totalPedidos = pedidos.length;
      });
    });
  }
}
