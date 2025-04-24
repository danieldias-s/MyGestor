import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { Chart, ChartData, ChartOptions, ChartType, registerables } from 'chart.js';


interface ChartDataset {
  data: number[];
  label: string;
  backgroundColor?: string | string[];
  borderColor?: string | string[];
  borderWidth?: number;
}

interface PieChartData extends ChartData<'doughnut', number[], string> {
  labels?: string[];
  datasets: {
    data: number[];
    backgroundColor?: string[];
    borderColor?: string[];
    borderWidth?: number;
  }[];
}

Chart.register(...registerables);

@Component({
  selector: 'app-dashboard',
  standalone:false,
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  cards = [
    { title: 'Clientes', count: 0, icon: '👥', color: '#4CAF50' },
    { title: 'Produtos', count: 0, icon: '📦', color: '#2196F3' },
    { title: 'Pedidos', count: 0, icon: '🧾', color: '#FF9800' },
    { title: 'Usuários', count: 0, icon: '🧑‍💼', color: '#9C27B0' },
    { title: 'Total de Vendas', count: 0, icon: '💰', color: '#0D47A1', isCurrency: true }
  ];


  barChartLabels: string[] = [];
  barChartData: ChartDataset[] = [
    { 
      data: [], 
      label: 'Pedidos por Mês',
      backgroundColor: '#FF9800',
      borderColor: '#E68A00',
      borderWidth: 1
    }
  ];
  barChartOptions: ChartOptions = {
    responsive: true,
    scales: { y: { beginAtZero: true } }
  };

 
  pieChartData: PieChartData = {
    labels: ['Finalizados', 'Processando', 'Cancelados'],
    datasets: [{
      data: [60, 30, 10],
      backgroundColor: ['#4CAF50', '#2196F3', '#F44336'],
      borderColor: ['#388E3C', '#1976D2', '#D32F2F'],
      borderWidth: 1
    }]
  };
  pieChartType: ChartType = 'doughnut';
  pieChartOptions: ChartOptions = {
    responsive: true
  };

  pedidosRecentes: any[] = [];
  isDarkMode = false;

  constructor(private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.dashboardService.getDashboardCounts().subscribe({
      next: counts => {
        this.cards[0].count = counts.clientes;
        this.cards[1].count = counts.produtos;
        this.cards[2].count = counts.pedidos;
        this.cards[3].count = counts.usuarios;
        this.cards[4].count = this.converterParaNumero(counts.totalVendas);
      }
    });
  
    this.dashboardService.getPedidosPorMes().subscribe({
      next: dados => {
        this.barChartLabels = dados.meses;
        this.barChartData[0].data = dados.valores;
      }
    });
  
    this.dashboardService.getPedidosRecentes().subscribe({
      next: pedidos => {
        this.pedidosRecentes = pedidos.map(p => ({
          ...p,
          total: this.converterParaNumero(p.total),
          dataPedido: this.converterParaData(p.dataPedido),
          dataFormatada: this.formatarData(p.dataPedido)
        }));
      }
    });
  }
  

  toggleTheme(): void {
    this.isDarkMode = !this.isDarkMode;
  }

  trackByPedidoId(index: number, pedido: any): number {
    return pedido.id;
  }
  converterParaNumero(valor: any): number {
    if (typeof valor === 'number') return valor;
    if (typeof valor === 'string') {
      
      const numero = parseFloat(valor.replace(/[R$\s]/g, '').replace('.', '').replace(',', '.'));
      return isNaN(numero) ? 0 : numero;
    }
    return 0;
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
  
  formatarData(data: any): string | null {
    const d = new Date(data);
    if (isNaN(d.getTime())) return null;
    return d.toLocaleDateString('pt-BR');
  }
  
}