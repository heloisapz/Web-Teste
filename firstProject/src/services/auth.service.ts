import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../app/models/Login';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.ApiUrl}/Usuario`; // Endpoint para login (verifique se a URL está correta)

  constructor(private http: HttpClient) { }

  // Função para realizar o login
  login(dados: Login): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, dados);  // Envia dados de login para o backend
  }

  // Salva o token no localStorage
  salvarToken(token: string): void {
    localStorage.setItem('authToken', token);  // Armazenando com o nome 'authToken' para melhor clareza
  }

  // Obtém o token armazenado
  obterToken(): string | null {
    return localStorage.getItem('authToken');  // Retorna o token salvo no localStorage
  }

  // Limpa o token do localStorage
  limparToken(): void {
    localStorage.removeItem('authToken');  // Remove o token do localStorage
  }
}
