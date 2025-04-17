import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { Usuario } from '../../app/models/Usuarios';
import { Response } from '../../app/models/Response';	
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = `${environment.ApiUrl}/Usuario`;

  constructor(private http: HttpClient) { }

  GetUsuarios(): Observable<Response<Usuario[]>> {
    return this.http.get<Response<Usuario[]>>(`${this.apiUrl}`);
  }

  CreateUsuario(usuario: Usuario): Observable<Response<Usuario[]>> {
    return this.http.post<Response<Usuario[]>>(`${this.apiUrl}`, usuario);
  }

  GetUsuario(id: number): Observable<Response<Usuario>> {
    return this.http.get<Response<Usuario>>(`${this.apiUrl}/${id}`);

  }

  EditarUsuario(usuario: Usuario): Observable<Response<Usuario[]>> {
    return this.http.put<Response<Usuario[]>>(`${this.apiUrl}`, usuario);
  }
}
