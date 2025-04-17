import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CadastroComponent } from './pages/cadastro/cadastro.component';
import { HomeComponent } from './pages/home/home.component';
import { EditarComponent } from './pages/editar/editar.component';

const routes: Routes = [
  {path: 'cadastro' , component: CadastroComponent},
  {path: '', component: HomeComponent}, 
  {path: 'editar/:id', component: EditarComponent},// Redireciona para o componente de cadastro na raiz
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
