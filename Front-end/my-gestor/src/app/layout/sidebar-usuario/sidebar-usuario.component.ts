import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-sidebar-usuario',
  standalone:false,
  templateUrl: './sidebar-usuario.component.html',
  styleUrls: ['./sidebar-usuario.component.css']
})
export class SidebarUsuarioComponent {
  constructor(private authService: AuthService, private router: Router) {}

  menuItems = [
    { label: 'Início', icon: '🏠', route: '/inicio-usuario' },
    { label: 'Clientes', icon: '👥', route: '/clientes' },
    { label: 'Pedidos', icon: '🧾', route: '/pedidos' },
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
