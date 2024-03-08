import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pedido } from 'src/app/models/pedido';
import { PedidoResponse } from 'src/app/models/pedidoResponse';
import { PedidoService } from 'src/app/services/Pedido/pedido.service';

@Component({
  selector: 'app-detalhes',
  templateUrl: './detalhes.component.html',
  styleUrls: ['./detalhes.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class DetalhesComponent implements OnInit {

  public id: string | null = '';
  public pedido!: PedidoResponse
  constructor(private router: Router, private route: ActivatedRoute, private servicePedido: PedidoService) {

  }
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')
    this.getPedido()
  }
  getPedido() {
    this.servicePedido.getPedido(this.id!).subscribe((x: PedidoResponse) => {
      this.pedido = x;
      console.log(this.pedido)
    })
  }
  returnToHome() {
    this.router.navigate(['/pedido'])
  }
}
