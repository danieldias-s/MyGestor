import { Component, OnInit } from '@angular/core';
import { UsuarioService } from '../../services/usuario.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from '../../models/usuario';

@Component({
  selector: 'app-usuarios',
  standalone: false,
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  usuarios: Usuario[] = [];
  form!: FormGroup;
  editando = false;
  usuarioId: number | null = null;

  constructor(private usuarioService: UsuarioService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', !this.editando ? Validators.required : null],
      role: ['Usuario', Validators.required]
    });
    this.carregarUsuarios();
  }

  carregarUsuarios() {
    this.usuarioService.listar().subscribe(data => this.usuarios = data);
  }

  salvar() {
    if (this.form.invalid) return;

    const usuario = this.form.value;

    if (this.editando && this.usuarioId !== null) {
      this.usuarioService.atualizar(this.usuarioId, usuario).subscribe(() => {
        this.resetarForm();
        this.carregarUsuarios();
      });
    } else {
      this.usuarioService.criar(usuario).subscribe(() => {
        this.resetarForm();
        this.carregarUsuarios();
      });
    }
  }

  editar(usuario: Usuario) {
    this.form.patchValue(usuario);
    this.editando = true;
    this.usuarioId = usuario.id;
  }

  excluir(id: number) {
    if (confirm('Tem certeza que deseja excluir este usuário?')) {
      this.usuarioService.deletar(id).subscribe(() => this.carregarUsuarios());
    }
  }

  resetarForm() {
    this.form.reset({ role: 'Usuario' });
    this.editando = false;
    this.usuarioId = null;
  }
}
