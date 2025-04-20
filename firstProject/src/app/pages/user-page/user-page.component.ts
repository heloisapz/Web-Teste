import { Component, OnInit } from '@angular/core';
import { Usuario } from '../../models/Usuarios';
import { UserService } from '../../../services/userService/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user-page',
  standalone: false,
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {
  btnAcao: string = 'Editar';
  btnTitulo: string = 'Editar usuário';
  usuario!: Usuario;

constructor(private userService: UserService, private route:ActivatedRoute, private router : Router) {

}
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.userService.GetUsuario(id).subscribe((data) => {
      this.usuario = data.dados;
      
    });
  }

  editarUsuario(usuario: Usuario) {
    this.userService.EditarUsuario(usuario).subscribe((data) => {
      this.router.navigate(['/']);
    });
  }

}
