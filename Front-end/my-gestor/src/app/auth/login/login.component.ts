import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup;
  erroLogin = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
  
    const { email, senha } = this.form.value;
  
    this.authService.login(email, senha).subscribe({
      next: () => {
        const role = this.authService.getUserRole();
        if (role === 'Admin') {
          this.router.navigate(['/dashboard']);
        } else if (role === 'Usuario') {
          this.router.navigate(['/inicio-usuario']);
        } else {
          this.erroLogin = true; // fallback se não reconhecer a role
        }
      },
      error: () => this.erroLogin = true
    });
  }
}