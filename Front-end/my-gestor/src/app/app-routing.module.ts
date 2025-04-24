import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LayoutComponent } from './layout/layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ClientesComponent } from './pages/clientes/clientes.component';
import { ProdutosComponent } from './pages/produtos/produtos.component';
import { PedidosComponent } from './pages/pedidos/pedidos.component';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';
import { ClienteFormComponent } from './pages/clientes/cliente-form/cliente-form.component';
import { ConfiguracoesComponent } from './pages/configuracoes/configuracoes.component';
import { InicioUsuarioComponent } from './pages/inicio-usuario/inicio-usuario.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },

  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },

      {
        path: 'clientes',
        component: ClientesComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Admin', 'Usuario'] }
      },
      {
        path: 'clientes/novo',
        component: ClienteFormComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Admin'] }
      },
      {
        path: 'clientes/editar/:id',
        component: ClienteFormComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Admin'] }
      },

      {
        path: 'produtos',
        component: ProdutosComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Admin', 'Usuario'] }
      },
      {
        path: 'pedidos',
        component: PedidosComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Admin', 'Usuario'] }
      },
      {
        path: 'usuarios',
        component: UsuariosComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Admin'] }
      },
      {
        path: 'configuracoes',
        component: ConfiguracoesComponent,
        canActivate: [RoleGuard],
        data: { roles: ['Admin'] }
      },
      {
        path: 'relatorios',
        loadChildren: () => import('./pages/relatorios/relatorios.module').then(m => m.RelatoriosModule),
        canActivate: [RoleGuard],
        data: { roles: ['Admin', 'Usuario'] }
      },
      {
        path: 'inicio-usuario',
        component: InicioUsuarioComponent,
        canActivate: [AuthGuard]
      }
      
      
      
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
