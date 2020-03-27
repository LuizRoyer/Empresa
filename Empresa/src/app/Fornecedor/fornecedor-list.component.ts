import { Component, OnInit } from '@angular/core';
import { FornecedorView } from '../Model/fornecedorView';
import { FornecedorService } from '../Services/fornecedorService';
import { Router } from '@angular/router';

@Component({
  templateUrl: './fornecedor-list.component.html',
})

export class FornecedorListComponent implements OnInit {

  _listaFornecedores: FornecedorView[] = [];
  _cpfFilter: string;
  _nomeFilter: string;
  _cnpjFilter: string;
  _dataFilter: Date;
  filterFornecedores: FornecedorView[];
  imagem = "/assets/img/pesquisa.jpg";

  constructor(private fornecedorService: FornecedorService, private router: Router) { }

  ngOnInit() {

    this.SelecionarFornecedores();
  }

  SelecionarFornecedores() {
    this.fornecedorService.SelecionarFornecedores().subscribe({
      next: fornecedor => {
        this._listaFornecedores = fornecedor;
        this.filterFornecedores = this._listaFornecedores;
      }, error: err => console.log('Erro', err)
    });
  }

  DeleteById(id: number): void {
    var result = confirm("Você deseja excluir esse registro");
    if (result === true) {

      this.fornecedorService.Deletar(id).subscribe(result => {
      }, error => console.log(error));
    }
  }

  set cnpjFiltro(value: string) {
    this._cnpjFilter = value;
    this.filterFornecedores = this._listaFornecedores.filter((fornecedor: FornecedorView) => fornecedor.cnpjEmpresa.toLocaleLowerCase().indexOf(this._cnpjFilter.toLocaleLowerCase()) > -1)
  }

  get cnpjFiltro() {
    return this._cnpjFilter
  }

  set nomeFiltro(value: string) {
    this._nomeFilter = value;
    this.filterFornecedores = this._listaFornecedores.filter((fornecedor: FornecedorView) => fornecedor.nomeFantasia.toLocaleLowerCase().indexOf(this._nomeFilter.toLocaleLowerCase()) > -1)
  }

  get nomeFiltro() {
    return this._nomeFilter
  }

  set cpfFiltro(value: string) {
    this._cpfFilter = value;
    this.filterFornecedores = this._listaFornecedores.filter((fornecedor: FornecedorView) => fornecedor.cpf.toLocaleLowerCase().indexOf(this._cpfFilter.toLocaleLowerCase()) > -1)
  }

  get cpfFiltro() {
    return this._cpfFilter
  }

  set DataFiltro(value: Date) {
    this._dataFilter = value;
    this.filterFornecedores = this._listaFornecedores.filter((fornecedor: FornecedorView) => fornecedor.dataCadastro.toString().indexOf(this._dataFilter.toString()) > -1)
  }

  get DataFiltro() {
    return this._dataFilter
  }

}