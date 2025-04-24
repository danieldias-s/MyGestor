import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../../../services/cliente.service';
import { Cliente } from '../../../models/cliente';


@Component({
  selector: 'app-cliente-form',
  standalone: false,
  templateUrl: './cliente-form.component.html',
  styleUrls: ['./cliente-form.component.css']
})
export class ClienteFormComponent implements OnInit {
  clienteForm: FormGroup;
  clienteId?: number;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.clienteForm = this.fb.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.clienteId = Number(this.route.snapshot.paramMap.get('id'));
    this.isEditMode = !!this.clienteId;

    if (this.isEditMode) {
      this.clienteService.getClienteById(this.clienteId).subscribe((cliente: Cliente) => {
        this.clienteForm.patchValue(cliente);
      });
    }
  }

  onSubmit(): void {
    if (this.clienteForm.invalid) return;

    const cliente: Cliente = {
      ...this.clienteForm.value,
      id: this.clienteId ?? 0
    };

    if (this.isEditMode) {
      this.clienteService.atualizarCliente(cliente.id,cliente).subscribe(() => {
        this.router.navigate(['/clientes']);
      });
    } else {
      this.clienteService.adicionarCLiente(cliente).subscribe(() => {
        this.router.navigate(['/clientes']);
      });
    }
  }
  voltar() {
    this.router.navigate(['/clientes']);
  }
  
}
