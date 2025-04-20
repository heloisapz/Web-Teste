import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl, ValidationErrors, AsyncValidatorFn } from '@angular/forms';
import { Usuario } from '../../models/Usuarios';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../../services/userService/user.service';
import { map, debounceTime, switchMap, first } from 'rxjs/operators';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html',
  styleUrls: ['./usuario-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class UsuarioFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<Usuario>();
  @Input() btnAcao!: string;
  @Input() btnTitulo!: string;
  @Input() dadosUsuario: Usuario | null = null;

  formularioUsuario!: FormGroup;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.formularioUsuario = new FormGroup(
      {
        id: new FormControl(this.dadosUsuario?.id ?? 0),
        nome: new FormControl(this.dadosUsuario?.nome ?? '', Validators.required),
        email: new FormControl(
          this.dadosUsuario?.email ?? '',
          [Validators.required, Validators.email],
          this.dadosUsuario ? [] : [this.verificarEmailValidator()] // somente se for novo
        ),
        tipo: new FormControl(this.dadosUsuario?.tipo ?? '', Validators.required),
        senha: new FormControl(this.dadosUsuario?.senha ?? '', Validators.required),
        confSenha: new FormControl('', Validators.required)
      },
      { validators: this.validarSenhasIguais }
    );
  }

  // Validador de confirmação de senha
  validarSenhasIguais(group: AbstractControl): ValidationErrors | null {
    const senha = group.get('senha')?.value;
    const confSenha = group.get('confSenha')?.value;
    if (senha && confSenha && senha !== confSenha) {
      return { senhaNaoConfere: true };
    }
    return null;
  }

  // Validador assíncrono de e-mail
  verificarEmailValidator(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(500),
        switchMap(email => this.userService.VerificarEmail(email)),
        map(existe => (existe ? { emailExistente: true } : null)),
        first()
      );
    };
  }

  submit(): void {
    if (this.formularioUsuario.valid) {
      const { confSenha, ...dados } = this.formularioUsuario.value;
      this.onSubmit.emit(dados as Usuario);
    } else {
      this.formularioUsuario.markAllAsTouched();
    }
  }
}
