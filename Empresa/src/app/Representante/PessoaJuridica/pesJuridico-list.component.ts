import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PesJuridico } from 'src/app/Model/pesJuridico';
import { PesJuridicaService } from 'src/app/Services/pesJuridicaService';

@Component({
  templateUrl: './pesJuridico-list.component.html',
})

export class PesJuridicoListComponent implements OnInit {

  _listaPesJuridico: PesJuridico[] = [];
  _cnpjFilter: string;
  _nomeFilter: string;
  filterPesJuridico: PesJuridico[];
  imagem = "/assets/img/pesquisa.jpg";
  
  constructor(private pesJuridicaService: PesJuridicaService, private router: Router) { }

  ngOnInit() {
    this.SelecionarRepresentantesJuridicos();
  }

  SelecionarRepresentantesJuridicos(): void {
    this.pesJuridicaService.SelecionarPesJuridicas(0).subscribe({
      next: juridica => {
        this._listaPesJuridico = juridica;
        this.filterPesJuridico = this._listaPesJuridico;
      }, error: err => console.log('Erro', err)
    });
  }

  DeleteById(id: number): void {
    var result = confirm("Você deseja excluir esse registro");
    if (result === true) {
      this.pesJuridicaService.Deletar(id).subscribe(result => {
      }, error => console.log(error));
    }
  }

  set cnpjFiltro(value: string) {
    this._cnpjFilter = value;
    this.filterPesJuridico = this._listaPesJuridico.filter((pesJuridica: PesJuridico) => pesJuridica.cnpj.toLocaleLowerCase().indexOf(this._cnpjFilter.toLocaleLowerCase()) > -1)
  }

  get cnpjFiltro() {
    return this._cnpjFilter;
  }

  set nomeFiltro(value: string) {
    this._nomeFilter = value;
    this.filterPesJuridico = this._listaPesJuridico.filter((pesJuridica: PesJuridico) => pesJuridica.nome.toLocaleLowerCase().indexOf(this._nomeFilter.toLocaleLowerCase()) > -1)
  }

  get nomeFiltro() {
    return this._nomeFilter;
  }
}