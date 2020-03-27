import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Contato } from '../Model/contato';
import { ContatoService } from '../Services/contatoService';
import { isNullOrUndefined } from 'util';

@Component({
  templateUrl: './contato.info.component.html'
})
export class ContatoInfoComponent implements OnInit {
  contato: Contato;

  constructor(private route: ActivatedRoute, private router: Router, private contatoService: ContatoService) { }
  ngOnInit(): void {
    this.contato = new Contato();
    this.contato.idPessoa = Number(this.route.snapshot.paramMap.get('id'));
  }

  Save(obj: Contato) {

    if (isNullOrUndefined(obj.celular) && isNullOrUndefined(obj.telefone)) {
      alert("Informe um Contato valido")
      return;
    }
    this.contatoService.Salvar(obj).subscribe(result => {
      console.log(result);

    }, (error: any) => { console.log(error) });

    this.router.navigate(['/contato/', obj.idPessoa])
  }

}