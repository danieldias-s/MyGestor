import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagesRoutingModule } from './pages-routing.module';
import { ClientesComponent } from './clientes/clientes.component';
import { ProdutosComponent } from './produtos/produtos.component';
import { PedidosComponent } from './pedidos/pedidos.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { ClienteFormComponent } from './clientes/cliente-form/cliente-form.component';
import { HomeUsuarioComponent } from './home-usuario/home-usuario.component';
import { PaginaInicialComponent } from './pagina-inicial/pagina-inicial.component';
import { EquipeComponentComponent } from './equipe-component/equipe-component.component';
import { ConfiguracoesComponent } from './configuracoes/configuracoes.component';
import { RelatorioDoMesComponent } from './relatorios/relatorio/relatorio.component';
import { InicioUsuarioComponent } from './inicio-usuario/inicio-usuario.component';


@NgModule({
  declarations: [
    ClientesComponent,
    ProdutosComponent,
    PedidosComponent,
    UsuariosComponent,
    DashboardComponent,
    UnauthorizedComponent,
    ClienteFormComponent,
    HomeUsuarioComponent,
    PaginaInicialComponent,
    EquipeComponentComponent,
    ConfiguracoesComponent,
    RelatorioDoMesComponent,
    InicioUsuarioComponent
  ],
  imports: [
    CommonModule,
    PagesRoutingModule
  ]
})
export class PagesModule { }
