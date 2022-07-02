import { Produto } from "../../modelo/produto";

export class LojaCarrinhoCompras {
    
  public produtos: Produto[] = [];

  public adicionar(produto: Produto) {
    var produtoLocalStorage = localStorage.getItem("produtoLocalStorage");

    if (!produtoLocalStorage) {
      //se não existir nada no localStorage -- adiciona o item
      this.produtos.push(produto);
    }
    else {
      //se pelo menos um unico item armazenado na sessão localStorage
      this.produtos = JSON.parse(produtoLocalStorage);
      this.produtos.push(produto);
    }
    localStorage.setItem("produtoLocalStorage", JSON.stringify(this.produtos));
  }

  public obterProdutos(): Produto[] {
    var produtoLocalStorage = localStorage.getItem("produtoLocalStorage");

    if (produtoLocalStorage)
      return JSON.parse(produtoLocalStorage);

    return this.produtos;
  }

  public removerProduto(produto: Produto) {
    var produtoLocalStorage = localStorage.getItem("produtoLocalStorage");

    if (produtoLocalStorage) {
      this.produtos = JSON.parse(produtoLocalStorage);
      this.produtos = this.produtos.filter(p => p.id != produto.id) // retorna para o array todos os produtos com excessao do filtrado pelo parametro do metodo
      localStorage.setItem("produtoLocalStorage", JSON.stringify(this.produtos));
    }
  }

  public atualizar(produtos: Produto[]) {
    localStorage.setItem("produtoLocalStorage", JSON.stringify(produtos));
  }

  public temItensCarrinhoCompras(): boolean {
    var itens = this.obterProdutos();
    return (itens.length > 0);
  }

  public limparCarrinhoCompras() {
    localStorage.setItem("produtoLocalStorage", "");
  }
}
