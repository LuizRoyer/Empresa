import { Component, OnInit } from '@angular/core';
import { Endereco } from '../Model/endereco';
import { EnderecoService } from '../Services/enderecoService';
import { Router } from '@angular/router';

@Component({
  templateUrl: './endereco-list.component.html',  
})

export class EnderecoListComponent implements OnInit {

  _listaEndereco: Endereco[] = [];
  _cepFilter: string;
  filterEndereco: Endereco[];
  imagem = "/assets/img/pesquisa.jpg";
  constructor(private enderecoService: EnderecoService, private router: Router) { }

  ngOnInit() {

    this.SelecionarEnderecos();
  }

  SelecionarEnderecos(): void {
    this.enderecoService.SelecionarEnderecos().subscribe({
      next: endereco => {
        this._listaEndereco = endereco;
        this.filterEndereco = this._listaEndereco;
      }, error: err => console.log('Erro', err)
    });
  }

  DeleteById(id: number): void {
    var result = confirm("Você deseja excluir esse registro");
    if (result === true) {
      this.enderecoService.Deletar(id).subscribe(result => {
      }, error => console.log(error));
    }
  }

  set cepFiltro(value: string) {
    this._cepFilter = value;
    this.filterEndereco = this._listaEndereco.filter((endero: Endereco) => endero.cep.toLocaleLowerCase().indexOf(this._cepFilter.toLocaleLowerCase()) > -1)
  }

  get cepFiltro() {
    return this._cepFilter
  }
}
