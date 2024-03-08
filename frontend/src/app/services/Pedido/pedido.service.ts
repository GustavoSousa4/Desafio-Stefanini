import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, retry, throwError } from 'rxjs';
import { Pedido } from 'src/app/models/pedido';
import { PedidoResponse } from 'src/app/models/pedidoResponse';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

  url = 'https://localhost:7190/Pedidos'
  constructor(private httpCliente: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  getPedidos(): Observable<PedidoResponse[]> {
    return this.httpCliente.get<PedidoResponse[]>(this.url + '/GetAll')
      .pipe(retry(2), catchError(err => throwError(err)));
  }

  getPedido(id: number): Observable<Pedido> {
    return this.httpCliente.get<Pedido>(this.url + '/' + id)
      .pipe(catchError(err => throwError(err)));
  }

  createPedido(pedido: Pedido): Observable<Pedido> {
    return this.httpCliente.post<Pedido>(this.url, JSON.stringify(pedido), this.httpOptions)
      .pipe(catchError(err => throwError(err)));
  }

  updatePedido(pedido: Pedido): Observable<Pedido> {
    return this.httpCliente.put<Pedido>(this.url + '/', JSON.stringify(pedido), this.httpOptions)
      .pipe(catchError(err => throwError(err)));
  }

  deletePedido(id: number) {
    return this.httpCliente.delete(this.url + '/' + id, this.httpOptions)
      .pipe(catchError(err => throwError(err)));
  }
}
