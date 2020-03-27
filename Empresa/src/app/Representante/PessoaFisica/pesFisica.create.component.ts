import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PesFisica } from '../../Model/pesFisica';
import { PesFisicaService } from '../../Services/pesFisicoServices';

@Component({
    templateUrl: './pesFisica.create.component.html'
})
export class PesFisicaCreateComponent implements OnInit {
    fisica: PesFisica = new PesFisica;

    constructor(private route: ActivatedRoute, private router: Router, private pesFisicaService: PesFisicaService) { }
    ngOnInit(): void { }

    Save(obj: PesFisica) {
        this.pesFisicaService.Salvar(obj).subscribe(result => {
            this.router.navigate(['/fisica']);
        }, (error: any) => { console.log(error) });
    }
}