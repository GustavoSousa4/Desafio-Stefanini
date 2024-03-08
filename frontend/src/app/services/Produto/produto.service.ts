import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, retry, throwError } from 'rxjs';
import { Produto } from 'src/app/models/produto';
import { ProdutoResponse } from 'src/app/models/produtoResponse';


@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  url = 'https://localhost:7190/Produto'

  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  getProdutos(): Observable<Produto[]> {
    return this.httpClient.get<Produto[]>(this.url + "/GetAll")
      .pipe(retry(2),
        catchError(err => throwError(err)));
  }

  getProductById(id: number): Observable<Produto> {
    return this.httpClient.get<Produto>(this.url + "/GetById/" + id)
  }

  createProduct(produto: Produto): Observable<Produto> {
    return this.httpClient.post<Produto>(this.url, JSON.stringify(produto), this.httpOptions)
      .pipe(retry(2), catchError(err => throwError(err)));
  }

  updateProduct(produto: Produto): Observable<Produto> {
    return this.httpClient.put<Produto>(this.url + "/" + produto.id, JSON.stringify(produto), this.httpOptions)
      .pipe(retry(2), catchError(err => throwError(err)));
  }

  deleteProduct(produto: Produto) {
    return this.httpClient.delete(this.url + "/" + produto.id, this.httpOptions)
      .pipe(retry(1), catchError(err => throwError(err)))
  }
}
