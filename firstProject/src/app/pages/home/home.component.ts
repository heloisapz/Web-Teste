import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../../services/userService/user.service';
import { Usuario } from '../../models/Usuarios';
import { Login } from '../../models/Login';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']  // Corrigido o nome para styleUrls
})
export class HomeComponent {

  loginData: Login = {email: '', senha: ''};
  erro: string = '';

  constructor(private authService: AuthService, private router: Router) {}
  
  fazerLogin() : void {
  this.authService.login(this.loginData).subscribe({
    next: (res) => {  
      this.authService.salvarToken(res.token); // Salva o token no localStorage
      const tipo = res.tipo; // Obtém o tipo de usuário do backend
      if (tipo === 1) {
        this.router.navigate(['/admin-page']); // Redireciona para a página de admin se o tipo for Admin
      } else if (tipo === 0) {
        this.router.navigate(['/']); // Redireciona para a página inicial se o tipo for Usuário
      }
    },
    error: () => {
      this.erro = 'Email ou senha incorretos'; // Mensagem de erro se o login falhar
    }
  })

}
}
