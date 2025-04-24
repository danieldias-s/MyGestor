import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Produto } from '../../models/produto';
import { ProdutosService } from '../../services/produto.service';


@Component({
  selector: 'app-produtos',
  standalone: false,
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {
  produtos: Produto[] = [];
  form: FormGroup;
  editando: boolean = false;
  idSelecionado: number | null = null;

  constructor(
    private produtosService: ProdutosService,
    private fb: FormBuilder
  ) {
    this.form = this.fb.group({
      nome: ['', Validators.required],
      preco: [0, Validators.required],
      estoque: [0, Validators.required]
    });
  }

  ngOnInit(): void {
    this.listar();
  }

  listar(): void {
    this.produtosService.listar().subscribe({
      next: (produtos) => this.produtos = [...produtos], // garante novo array
      error: (err) => console.error('Erro ao listar produtos:', err)
    });
  }
  

  salvar(): void {
    if (this.form.invalid) return;

    const produto = this.form.value as Produto;

    if (this.editando && this.idSelecionado !== null) {
      this.produtosService.atualizar(this.idSelecionado, produto).subscribe(() => {
        this.cancelar();
        this.listar();
      });
    } else {
      this.produtosService.criar(produto).subscribe(() => {
        this.cancelar();
        this.listar();
      });
    }
  }

  editar(produto: Produto): void {
    this.form.setValue({
      nome: produto.nome,
      preco: produto.preco,
      estoque: produto.estoque
    });
    this.editando = true;
    this.idSelecionado = produto.id;
  }

  excluir(id: number): void {
    this.produtosService.excluir(id).subscribe(() => this.listar());
  }

  cancelar(): void {
    this.editando = false;
    this.idSelecionado = null;
    this.form.reset();
  }
}
