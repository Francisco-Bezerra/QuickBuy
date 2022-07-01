import { Component, OnInit } from "@angular/core";
import { Produto } from "../modelo/produto";
import { ProdutoServico } from "../servicos/produto/produto.servico";
import { Router } from "@angular/router";

@Component({
  selector: "app-produto",
  templateUrl: "./produto.component.html",
  styleUrls: ["./produto.component.css"]
})

export class ProdutoComponent implements OnInit{
    
  public produto: Produto;
  arquivoSelecionado: File;
  ativar_spinner: boolean;
  mensagem: string;
  inputFileText: string = 'Escolha uma foto para o produto';

  constructor(private produtoServico: ProdutoServico, private router: Router) { }

  public inputChange(files: FileList) {
    this.arquivoSelecionado = files.item(0);
    this.produtoServico.enviarArquivo(this.arquivoSelecionado)
      .subscribe(
        nomeArquivo => {
          this.produto.nomeArquivo = nomeArquivo;
          console.log(nomeArquivo);
          this.inputFileText = nomeArquivo;
        },
        erro => {
          console.log(erro.error);
        }
      );
  }

  ngOnInit(): void {
    var produtoSession = sessionStorage.getItem('produtoSession');

    if (produtoSession)
      this.produto = JSON.parse(produtoSession);
    else
      this.produto = new Produto();
  }

  public cadastrar() {
    this.ativarEspera();
    this.produtoServico.cadastrar(this.produto)
      .subscribe(
        produtoJSON => {
          console.log(produtoJSON);
          this.desativarEspera();
          this.router.navigate(['pesquisa-produto']);
        },
        err => {
          console.log(err.error);
          this.desativarEspera();
          this.mensagem = err.error;
        }
      );
  }

  public ativarEspera() {
    this.ativar_spinner = true;
  }
  public desativarEspera() {
    this.ativar_spinner = false;
  }

}
