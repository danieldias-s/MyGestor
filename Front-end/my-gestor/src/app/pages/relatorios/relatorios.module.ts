import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RelatorioComponent } from './relatorio/relatorio.component';
import { RelatoriosRoutingModule } from './relatorios-routing.module';


@NgModule({
  declarations: [RelatorioComponent],
  imports: [
    CommonModule,
    FormsModule,
    RelatoriosRoutingModule
  ]
})
export class RelatoriosModule {}
