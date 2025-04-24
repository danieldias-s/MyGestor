import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



import { ClientesComponent } from './pages/clientes/clientes.component';
import { ProdutosComponent } from './pages/produtos/produtos.component';
import { PedidosComponent } from './pages/pedidos/pedidos.component';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';
import { BaseChartDirective } from 'ng2-charts';


import { RoleGuard } from './guards/role.guard';
import { AuthGuard } from './guards/auth.guard';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthModule } from './auth/auth.module';
import { ClienteFormComponent } from './pages/clientes/cliente-form/cliente-form.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { LayoutComponent } from './layout/layout.component';
import { SharedModule } from './shared/shared.module';
import { SidebarUsuarioComponent } from './layout/sidebar-usuario/sidebar-usuario.component';
import { InicioUsuarioComponent } from './pages/inicio-usuario/inicio-usuario.component';




@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ClientesComponent,
    ProdutosComponent,
    PedidosComponent,
    UsuariosComponent,
    UnauthorizedComponent,
    ClienteFormComponent,
    SidebarComponent,
    LayoutComponent,
    SidebarUsuarioComponent,
    InicioUsuarioComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AuthModule,
    BaseChartDirective,
    SharedModule
    
    
  ],
  providers: [
    AuthGuard,
    RoleGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
