import { Component, Inject, OnInit } from '@angular/core';
import { UserService } from '../../../services/userService/user.service';
import { Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Usuario } from '../../models/Usuarios';

@Component({
  selector: 'app-excluir',
  standalone: false,
  templateUrl: './excluir.component.html',
  styleUrl: './excluir.component.css'
})
export class ExcluirComponent implements OnInit {

  inputdata: any;
  usuario!: Usuario; 

  constructor(
    private usuarioService: UserService, // Corrigido o nome para UserService
    private router: Router, // Adicionado o Router para redirecionamento
    @Inject(MAT_DIALOG_DATA) public data: any, // Adicionado o Inject para receber dados do diálogo
    private ref: MatDialogRef<ExcluirComponent> // Adicionado o MatDialogRef para fechar o diálogo
  ) {
    // Inicialização do componente
  }

  ngOnInit(): void {
    
    this.inputdata = this.data; // Acessando o ID passado pelo diálogo
    this.usuarioService.GetUsuario(this.inputdata.id).subscribe((data) => {
      this.usuario = data.dados; // Acessando os dados do usuário
    });

  }

  Excluir() {
    this.usuarioService.ExcluirUsuario(this.inputdata.id).subscribe((data) => {
     this.ref.close(); // Fecha o diálogo após a exclusão
     window.location.reload(); // Atualiza a página para refletir as alterações
    })
  }

}
