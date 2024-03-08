import { Component, OnInit } from '@angular/core';
import { Pedido } from '../models/pedido';
import { PedidoService } from '../services/Pedido/pedido.service';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ItensPedido } from '../models/itensPedido';
import { PedidoResponse } from '../models/pedidoResponse';
import { ProdutoService } from '../services/Produto/produto.service';
import { Produto } from '../models/produto';

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.scss']
})
export class PedidoComponent implements OnInit {

  public form: FormGroup = new FormGroup({});
  public pedido = {} as Pedido
  public pedidos: PedidoResponse[] = []
  public produtos: Produto[] = [];
  public produto: number = 0;


  constructor(private produtoService: ProdutoService, private pedidoService: PedidoService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.getPedidos();
    this.form = this.formBuilder.group({
      nomeCliente: new FormControl('', Validators.required),
      emailCliente: new FormControl('', Validators.required),
      pago: new FormControl('', Validators.required),
      quantidadeItem: new FormControl('', Validators.required),
      itemId: new FormControl('', Validators.required),
      produto: new FormControl('')
    });
    this.getProdutos()
  }
  returnToHome() {
    this.router.navigate(['/']);
  }

  getPedidos() {
    this.pedidoService.getPedidos().subscribe((pedidoResponse: PedidoResponse[]) => {
      console.log(pedidoResponse)
      this.pedidos = pedidoResponse
    })
  }

  public createOrUpdatePedido() {
    console.log(this.form.controls['produto'].value)
    var request = {
      nomeCliente: this.form.controls['nomeCliente'].value,
      emailCliente: this.form.controls['emailCliente'].value,
      pago: this.form.controls['pago'].value,
      item: [{
        quantidade: this.form.controls['quantidadeItem'].value,
        idProduto: this.form.controls['produto'].value
      } as ItensPedido]
    } as Pedido;
    console.log(request);
    this.pedidoService.createPedido(request).subscribe(() => {
    })
    window.location.reload()
  }

  deletePedido(id: number) {
    this.pedidoService.deletePedido(id).subscribe(() => {
      this.getPedidos();
    })
    window.location.reload()

  }

  getProdutos() {
    this.produtoService.getProdutos().subscribe((produtos: Produto[]) => {
      console.log(produtos);
      this.produtos = produtos;
    });
  }

  editPedido(pedido: any) { // Método para editar o pedido
    // Preenche os campos do formulário com os dados do pedido
    this.form.patchValue({
      nomeCliente: pedido.nomeCliente,
      emailCliente: pedido.emailCliente,
      pago: pedido.pago,
      quantidadeItem: pedido.itensPedidos[0].quantidade,
      produto: pedido.itensPedidos[0].idProduto
    });
  }

}
