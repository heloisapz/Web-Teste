import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../app/models/Login';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private apiUrl = `${environment.ApiUrl}/Usuario`; // É comum ter um endpoint '/auth/login'

  constructor(private http: HttpClient) { }

  login(dados: Login): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, dados);
  }

  salvarToken(token: string): void {

    localStorage.setItem('token', token);

  }

  obterToken(): string | null {
    return localStorage.getItem('token');
  }

  limparToken(): void {
    localStorage.removeItem('token');
  }
}