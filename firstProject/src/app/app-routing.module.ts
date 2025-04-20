import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CadastroComponent } from './pages/cadastro/cadastro.component';
import { HomeComponent } from './pages/home/home.component';
import { EditarComponent } from './pages/editar/editar.component';
import { AdminPageComponent } from './pages/admin-page/admin-page.component';
import { UserPageComponent } from './pages/user-page/user-page.component';

const routes: Routes = [
  {path: 'cadastro' , component: CadastroComponent},
  {path: '', component: HomeComponent}, 
  {path: 'editar/:id', component: EditarComponent},
  {path: 'admin-page', component: AdminPageComponent},// Redireciona para o componente de cadastro na raiz
  {path: 'user-page/:id', component: UserPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
