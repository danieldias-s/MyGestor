import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: false,
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  constructor(private authService: AuthService, private router: Router) {}

  menuItems = [
    { label: 'Dashboard', icon: '📊', route: '/dashboard' },
    { label: 'Clientes', icon: '👥', route: '/clientes' },
    { label: 'Pedidos', icon: '🧾', route: '/pedidos' },
    { label: 'Produtos', icon: '📦', route: '/produtos' },
    { label: 'Financeiro', icon: '💰', route: '/financeiro' },
    { label: 'Relatórios', icon: '📈', route: '/relatorios' },
    { label: 'Usuarios', icon: '🧑‍💼', route: '/usuarios' },
    { label: 'Configurações', icon: '⚙️', route: '/configuracoes' },
    { label: 'Sair', icon: '🚪', action: 'logout' }
  ];

  onMenuClick(item: any): void {
    if (item.action === 'logout') {
      this.authService.logout();
    } else if (item.route) {
      this.router.navigate([item.route]);
    }
  }
}