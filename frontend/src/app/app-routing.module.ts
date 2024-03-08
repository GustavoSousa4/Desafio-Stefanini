import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PedidoComponent } from './pedido/pedido.component';
import { ProdutoComponent } from './produto/produto.component';
import { HomeComponent } from './home/home.component';
import { DetalhesComponent } from './pedido/detalhes/detalhes.component';
const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'produto',
    component: ProdutoComponent
  },
  {
    path: 'pedido',
    component: PedidoComponent
  },
  {
    path: 'detalhes/:id',
    component: DetalhesComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
