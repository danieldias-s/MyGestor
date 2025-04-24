import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FiltroClientePipe } from './pipes/filtro-cliente.pipe';


@NgModule({
  declarations: [FiltroClientePipe],
  imports: [CommonModule],
  exports: [FiltroClientePipe]
})
export class SharedModule {}