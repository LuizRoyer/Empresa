import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Fornecedor } from '../Model/fornecedor';
import { FornecedorService } from '../Services/fornecedorService';
import { PesJuridico } from '../Model/pesJuridico';
import { PesFisica } from '../Model/pesFisica';
import { Empresa } from '../Model/Empresa';
import { EmpresaService } from '../Services/empresaService';
import { PesJuridicaService } from '../Services/pesJuridicaService';
import { PesFisicaService } from '../Services/pesFisicoServices';
import { isNullOrUndefined } from 'util';

@Component({
    templateUrl: './fornecedor.create.component.html'
})
export class FornecedorCreateComponent implements OnInit {
    fornecedor: Fornecedor = new Fornecedor;
    listaPesJurudiaca: PesJuridico[] = [];
    listaPesFisica: PesFisica[] = [];
    listaEmpresas: Empresa[] = [];

    constructor(private route: ActivatedRoute, private router: Router,
        private fornecedorService: FornecedorService, private empresaService: EmpresaService,
        private pesFisicaService: PesFisicaService, private pesJuridicaService: PesJuridicaService) { }

    ngOnInit(): void {

        this.CarregarListaEmpresas();
        this.CarregarListaPessoasFisicas();
        this.CarregarListaPessoasJuridicas();
    }

    onNavigate(location: string) { this.router.navigate([`/${location}`]) }

    Save(obj: Fornecedor) {

        if (this.Validar(obj)) {

            this.fornecedorService.Salvar(obj).subscribe(result => {
                console.log(result);
                this.router.navigate(['/fornecedor']);
            }, (error: any) => { console.log(error) });
        }
    }

    CarregarListaEmpresas() {
        this.empresaService.SelecionarEmpresas().subscribe({
            next: empresa => {
                this.listaEmpresas = empresa;
            }, error: err => console.log('Erro', err)
        });
    }

    CarregarListaPessoasFisicas() {
        this.pesFisicaService.SelecionarPesFisicas(0).subscribe({
            next: fisica => {
                this.listaPesFisica = fisica;
            }, error: err => console.log('Erro', err)
        });
    }
    CarregarListaPessoasJuridicas() {
        this.pesJuridicaService.SelecionarPesJuridicas(0).subscribe({
            next: juridica => {
                this.listaPesJurudiaca = juridica;
            }, error: err => console.log('Erro', err)
        });
    }

    Validar(obj: Fornecedor): boolean {

        const pessoa = this.listaPesFisica.filter((fisica: PesFisica) => fisica.idPessoa.toString().indexOf(obj.idPessoa.toString()) > -1)

        if (!isNullOrUndefined(pessoa)) {

            const empresa = this.listaEmpresas.filter((empresa: Empresa) => empresa.idEmpresa.toString().indexOf(obj.idEmpresa.toString()) > -1)

            if (empresa[0].endereco.uf === "PR"
                && this.ObterIdade(pessoa[0].dataNascimento) < 18) {
                alert("Estado do Paraná não permite Cadastrar pessoa física menor de idade");
                return false;
            }
        }
        return true;
    }
    ObterIdade(dateNascimento) {
        var birthday = +new Date(dateNascimento);
        return (~~((Date.now() - birthday) / (31557600000)));
    }
}