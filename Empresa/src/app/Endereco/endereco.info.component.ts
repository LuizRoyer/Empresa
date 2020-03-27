import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Endereco } from '../Model/endereco';
import { EnderecoService } from '../Services/enderecoService';
import { isNullOrUndefined } from 'util';
import { Estado } from '../Model/estado';

@Component({
  templateUrl: './endereco.info.component.html'
})
export class EnderecoInfoComponent implements OnInit {
  endereco: Endereco;
  listaEstados: Estado[] = [];

  constructor(private route: ActivatedRoute, private router: Router, private enderecoService: EnderecoService) { }
  ngOnInit(): void {

    this.enderecoService.SelecionarEndereco(parseInt(this.route.snapshot.paramMap.get('id'))).subscribe({
      next: dados => {
        this.endereco = dados;
      }, error: err => console.log('Erro', err)
    });

    this.enderecoService.SelecionarEstados().subscribe({
      next: estado => {
        this.listaEstados = estado;
      }, error: err => console.log('Erro', err)
    });

  }
  Save(obj: Endereco) {
    this.enderecoService.Salvar(obj).subscribe(result => {
      console.log(result);      
    }, (error: any) => { console.log(error) });
    this.router.navigate(['/endereco']);
  }

  BuscarCep(cep: string) {
    this.enderecoService.BuscarCep(cep).subscribe({
      next: dados => {
        this.endereco = dados;
        this.BuscarEstado(this.endereco.uf);
      }, error: err => console.log('Erro', err)
    });
  }
  BuscarEstado(uf: string) {
    const estado = this.listaEstados.filter((estado: Estado) => estado.uf.toLocaleLowerCase().indexOf(uf.toLocaleLowerCase()) > -1);
    if (!isNullOrUndefined(estado)) {
      this.endereco.nomeEstado = estado[0].nome;
    }
  }
}