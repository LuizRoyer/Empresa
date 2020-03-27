import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PesFisica } from '../../Model/pesFisica';
import { PesFisicaService } from '../../Services/pesFisicoServices';

@Component({
  templateUrl: './pesFisico-list.component.html',
})

export class PesFisicaListComponent implements OnInit {

  _listaPesFisica: PesFisica[] = [];
  _cpfFilter: string;
  _nomeFilter: string;
  filterPesFisica: PesFisica[];
  imagem = "/assets/img/pesquisa.jpg";
  constructor(private pesFisicaService: PesFisicaService, private router: Router) { }

  ngOnInit() {
    this.SelecionarRepresentantesFisicos();
  }

  SelecionarRepresentantesFisicos(): void {
    this.pesFisicaService.SelecionarPesFisicas(0).subscribe({
      next: fisica => {
        this._listaPesFisica = fisica;
        console.log(fisica);
        this.filterPesFisica = this._listaPesFisica;
      }, error: err => console.log('Erro', err)
    });
  }

  DeleteById(id: number): void {
    var result = confirm("Você deseja excluir esse registro");
    if (result === true) {
      this.pesFisicaService.Deletar(id).subscribe(result => {
      }, error => console.log(error));
    }
  }

  set cpfFiltro(value: string) {
    this._cpfFilter = value;
    this.filterPesFisica = this._listaPesFisica.filter((pesFisica: PesFisica) => pesFisica.cpf.toLocaleLowerCase().indexOf(this._cpfFilter.toLocaleLowerCase()) > -1)
  }

  get cpfFiltro() {
    return this._cpfFilter;
  }

  set nomeFiltro(value: string) {
    this._nomeFilter = value;
    this.filterPesFisica = this._listaPesFisica.filter((pesFisica: PesFisica) => pesFisica.nome.toLocaleLowerCase().indexOf(this._nomeFilter.toLocaleLowerCase()) > -1)
  }

  get nomeFiltro() {
    return this._nomeFilter;
  }

}