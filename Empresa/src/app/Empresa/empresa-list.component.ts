import { Component, OnInit } from '@angular/core';
import { Empresa } from '../Model/Empresa';
import { EmpresaService } from '../Services/empresaService';
import { Router } from '@angular/router';

@Component({
  templateUrl: './empresa-list.component.html',
})

export class EmpresaListComponent implements OnInit {

  _listaEmpresa: Empresa[] = [];
  _cepFilter: string;
  _nomeFilter: string;
  filterEmpresa: Empresa[];
  imagem = "/assets/img/pesquisa.jpg";
  constructor(private empresaService: EmpresaService, private router: Router) { }

  ngOnInit() {

    this.SelecionarEmpresas();
  }

  SelecionarEmpresas() {
    this.empresaService.SelecionarEmpresas().subscribe({
      next: empresa => {
        this._listaEmpresa = empresa;
        this.filterEmpresa = this._listaEmpresa;
      }, error: err => console.log('Erro', err)
    });
  }

  DeleteById(id: number): void {
    var result = confirm("Você deseja excluir esse registro");
    if (result === true) {
      this.empresaService.Deletar(id).subscribe(result => {
      }, error => console.log(error));
    }
  }

  set cepFiltro(value: string) {
    this._cepFilter = value;
    this.filterEmpresa = this._listaEmpresa.filter((empresa: Empresa) => empresa.endereco.cep.toLocaleLowerCase().indexOf(this._cepFilter.toLocaleLowerCase()) > -1)
  }

  get cepFiltro() {
    return this._cepFilter
  }

  set nomeFiltro(value: string) {
    this._nomeFilter = value;
    this.filterEmpresa = this._listaEmpresa.filter((empresa: Empresa) => empresa.nomeFantasia.toLocaleLowerCase().indexOf(this._nomeFilter.toLocaleLowerCase()) > -1)
  }

  get nomeFiltro() {
    return this._nomeFilter
  }
}