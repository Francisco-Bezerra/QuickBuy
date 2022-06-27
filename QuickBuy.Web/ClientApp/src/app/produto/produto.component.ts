import { Component } from "@angular/core";

@Component({
  selector: "app-produto",
  template: "<html><body>{{ obterNome() }}</body></html>"
})

export class ProdutoComponent {//NOme das classe começando em maísculo por conta da convenção PascalCase
  public nome: string;
  public liberadoParaVenda: boolean;

  //camelCase para variáveis, atributos e nomes das funções
  obterNome(): string {
    return "Xioami";
  }
}
