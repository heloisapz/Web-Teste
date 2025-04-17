import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/userService/user.service'; // Corrigido o nome para UserService
import { Usuario } from '../../models/Usuarios';

@Component({
  selector: 'app-admin-page',
  standalone: false,
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.css'
})
export class AdminPageComponent {
usuarios: Usuario[] = []; 
  usuarioGeral: Usuario[] = []; 

  constructor(private usuarioService: UserService) { 

  }

  ngOnInit(): void {
    
    this.usuarioService.GetUsuarios().subscribe(data => {
      const dados = data.dados;
      this.usuarios = dados;
      this.usuarioGeral = dados;

  })

}
search(event: Event): void {
  const target = event.target as HTMLInputElement;
  const value = target.value.toLowerCase(); // converte valor para lowercase

  this.usuarios = this.usuarioGeral.filter(usuario => {  
    return usuario.nome.toLowerCase().includes(value) || usuario.email.toLowerCase().includes(value) ; // faz comparação em lowercase
  });
}


}
