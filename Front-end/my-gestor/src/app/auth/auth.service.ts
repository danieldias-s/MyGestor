import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5221/api/auth'; // Atualize para a URL correta da sua API

  constructor(private http: HttpClient, private router: Router) {}

  login(email: string, senha: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, { email, senha }).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
      })
    );
  }

  logout(): void {  
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;

    const payload = this.getTokenPayload(token);
    if (!payload) return false;

    const exp = payload.exp;
    const now = Math.floor(new Date().getTime() / 1000);
    return exp && exp > now;
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUserRole(): string | null {
    const token = this.getToken();
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      console.log(payload);
    }
    const payload = this.getTokenPayload(token);
    return payload?.role || null;
  }

  getUserEmail(): string {
    const token = this.getToken();
    const payload = this.getTokenPayload(token);
    return payload?.email || null;
  }

  private getTokenPayload(token: string | null): any {
    if (!token) return null;
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload));
    } catch {
      return null;
    }
  }
}
