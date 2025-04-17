import { Component } from '@angular/core';
import { Usuario } from '../../models/Usuarios';
import { Router } from '@angular/router';
import { UserService } from '../../../services/userService/user.service';

@Component({
  selector: 'app-cadastro',
  standalone: false,
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css'] // <- corrigido
})
export class CadastroComponent {
  btnAcao = "Cadastrar";
  btnTitulo = "Cadastrar Usuário";

constructor(private userService: UserService, private router: Router) { 

}

createUsuario(usuario: Usuario)
{
  this.userService.CreateUsuario(usuario).subscribe((data) => { 
    this.router.navigate(['/']);

});
}

}


