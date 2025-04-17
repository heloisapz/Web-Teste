import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Usuario } from '../../models/Usuarios';

@Component({
  selector: 'app-usuario-form',
  standalone: false,
  templateUrl: './usuario-form.component.html',
  styleUrl: './usuario-form.component.css'
})
export class UsuarioFormComponent implements OnInit{
  @Output() onSubmit = new EventEmitter<Usuario>();
  @Input() btnAcao!: string;
  @Input() btnTitulo!: string;
  @Input() dadosUsuario: Usuario |null = null;

  formularioUsuario!: FormGroup

constructor() 
{
  
}
  ngOnInit(): void 
  {

    this.formularioUsuario = new FormGroup({
      'id': new FormControl(this.dadosUsuario?this.dadosUsuario.id : 0),
      'nome': new FormControl(this.dadosUsuario?this.dadosUsuario.nome : '', Validators.required),
      'email': new FormControl(this.dadosUsuario?this.dadosUsuario.email : '', Validators.required), 
      'tipo': new FormControl(this.dadosUsuario?this.dadosUsuario.tipo : '', Validators.required),
      'senha': new FormControl(this.dadosUsuario?this.dadosUsuario.senha : '',Validators.required),
      'confSenha': new FormControl('', Validators.required),    
    });
  }

  submit()
  {
    this.onSubmit.emit(this.formularioUsuario.value);
  }
}
