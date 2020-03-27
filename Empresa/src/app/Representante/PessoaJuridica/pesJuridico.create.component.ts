import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PesJuridico } from 'src/app/Model/pesJuridico';
import { PesJuridicaService } from 'src/app/Services/pesJuridicaService';

@Component({
    templateUrl: './pesJuridico.create.component.html'
})
export class PesJuridicoCreateComponent implements OnInit {
    juridica: PesJuridico = new PesJuridico;

    constructor(private route: ActivatedRoute, private router: Router, private pesJuridicaService: PesJuridicaService) { }
    ngOnInit(): void { }

    Save(obj: PesJuridico) {
        this.pesJuridicaService.Salvar(obj).subscribe(result => {
        }, (error: any) => { console.log(error) });
        this.router.navigate(['/juridica']);
    }
}