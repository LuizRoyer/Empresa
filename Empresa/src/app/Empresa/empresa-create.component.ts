import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { isNullOrUndefined } from 'util';
import { Empresa } from '../Model/Empresa';
import { Endereco } from '../Model/endereco';
import { EmpresaService } from '../Services/empresaService';
import { EnderecoService } from '../Services/enderecoService';

@Component({
    templateUrl: './empresa-create.component.html'
})
export class EmpresaCreateComponent implements OnInit {
    empresa: Empresa = new Empresa;
    listaEnderecos: Endereco[];
    constructor(private route: ActivatedRoute, private router: Router, private empresaService: EmpresaService, private enderecoService: EnderecoService) { }
    ngOnInit(): void {

        this.enderecoService.SelecionarEnderecos().subscribe({
            next: endereco => {
                this.listaEnderecos = endereco;
            }, error: err => console.log('Erro', err)
        });
    }

    Save(obj: Empresa) {
        obj.endereco = this.listaEnderecos.filter((endereco: Endereco) => endereco.idEndereco.toString().indexOf(obj.filtro.toString()) > -1)[0];
        if (isNullOrUndefined(obj.endereco)) {
            alert('Informe um Endereço')
            return;
        } else {
            this.empresaService.Salvar(obj);
            this.router.navigate(['/empresa']);
        }
    }
}