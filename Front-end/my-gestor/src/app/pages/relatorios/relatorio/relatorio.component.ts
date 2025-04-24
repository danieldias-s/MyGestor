import { Component } from '@angular/core';
import { RelatorioService } from '../../../services/relatorio.service';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { saveAs as fileSaveAs } from 'file-saver';



@Component({
  selector: 'app-relatorio',
  standalone:false,
  templateUrl: './relatorio.component.html',
  styleUrls: ['./relatorio.component.css']
})
export class RelatorioComponent {
  ano = new Date().getFullYear();
  mes = new Date().getMonth() + 1;
  resultados: any[] = [];

  constructor(private relatorioService: RelatorioService) {}

  buscarRelatorio(): void {
    this.relatorioService.getRelatorio(this.ano, this.mes).subscribe({
      next: data => this.resultados = data,
      error: err => console.error(err)
    });
  }
  exportarExcel(): void {
    const worksheet = XLSX.utils.json_to_sheet(this.resultados.map(item => ({
      Cliente: item.cliente?.nome,
      Data: new Date(item.dataPedido).toLocaleString(),
      Total: item.total
    })));
  
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Relatorio');
  
    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
fileSaveAs(blob, `relatorio-${this.ano}-${this.mes}.xlsx`);

  }
  exportarPDF(): void {
    const doc = new jsPDF();
  
    const dados = this.resultados.map(item => [
      item.cliente?.nome,
      new Date(item.dataPedido).toLocaleString(),
      `R$ ${item.total.toFixed(2)}`
    ]);
  
    autoTable(doc, {
      head: [['Cliente', 'Data', 'Total']],
      body: dados,
    });
  
    doc.save(`relatorio-${this.ano}-${this.mes}.pdf`);
  }
  
  
}

