import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { Login } from '../../models/Login';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  loginData: Login = { Email: '', Senha: '' };
  erro: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  // Função de login
  fazerLogin(): void {
    this.authService.login(this.loginData).subscribe({
      next: (res) => {
        if (res && res.dados && res.dados.token && res.dados.tipo !== undefined) {
          this.authService.salvarToken(res.dados.token);
          this.redirecionar(res.dados.tipo, res.dados.id);  // <-- passa o ID aqui
        } else {
          this.erro = 'Resposta inesperada do servidor.';
        }
      },
      error: () => {
        this.erro = 'Email ou senha incorretos';
      }
    });
  }
  
  private redirecionar(tipo: number, id: number): void {
    if (tipo === 1) {
      this.router.navigate(['/admin-page']);
    } else {
      this.router.navigate(['/user-page', id]);  // <-- navegação correta
    }
  }
}
