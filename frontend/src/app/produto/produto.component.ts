import { Component, OnInit } from '@angular/core';
import { Produto } from '../models/produto';
import { ProdutoService } from '../services/Produto/produto.service';
import { FormBuilder, NgForm } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss']
})
export class ProdutoComponent implements OnInit {

  public produto = {} as Produto;
  public produtos: Produto[] = [];
  constructor(private produtoService: ProdutoService, builder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.getProdutos()
  }

  returnToHome() {
    this.router.navigate(['/']);
  }

  updateProduct(form: NgForm) {
    if (this.produto.id !== undefined) {
      this.produtoService.updateProduct(this.produto).subscribe(() => {
        form.resetForm();
      })
      window.location.reload()
    }
    else {
      this.produtoService.createProduct(this.produto).subscribe(() => {
        form.resetForm();
      })
      window.location.reload()

    }
  }
  getProdutos() {
    this.produtoService.getProdutos().subscribe((produtos: Produto[]) => {
      console.log(produtos);
      this.produtos = produtos;
    });
  }

  deleteProduct(produto: Produto) {
    this.produtoService.deleteProduct(produto).subscribe(() => {
      this.getProdutos();
    })
    window.location.reload()
  }

  editProduct(produto: Produto) {
    this.produto = { ...produto }
  }
}
