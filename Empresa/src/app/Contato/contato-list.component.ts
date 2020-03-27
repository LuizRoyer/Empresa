import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ContatoService } from '../Services/contatoService';
import { Contato } from '../Model/contato';

@Component({
  templateUrl: './contato-list.component.html',
})

export class ContatoListComponent implements OnInit {

  listaContato: Contato[] = [];
 idpessoa:number;
  constructor(private contatoService: ContatoService,private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    
    this.SelecionarContatos();
  }
  SelecionarContatos(): void {
    this.idpessoa = Number(this.route.snapshot.paramMap.get('id'));
    this.contatoService.SelecionarContato(this.idpessoa).subscribe({
      next: contato => {
        this.listaContato = contato;
        console.log(contato);
       
      }, error: err => console.log('Erro', err)
    });
  }

  DeleteById(id: number): void {
    var result = confirm("Você deseja excluir esse registro");
    if (result === true) {
      this.contatoService.Deletar(id,0).subscribe(result => {
      }, error => console.log(error));
    }
  }



}