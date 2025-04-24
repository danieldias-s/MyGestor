export interface Pedido {
  id: number;
  clienteId: number;
  clienteNome: string;
  dataPedido: Date | null; 
  total:number;
  itens: PedidoItem[]; 
}

export interface PedidoItem {
  produtoId: number;
  produtoNome: string;
  quantidade: number;
  precoUnitario: number;
}