import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Empresa } from '../Model/empresa';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class EmpresaService {

    constructor(private httpClient: HttpClient) { }

    SelecionarEmpresas() {
        return this.httpClient.get<Empresa[]>(`/api/Empresa/SelecionarEmpresas`);
    }
    
    SelecionarEmpresa(idEmpresa: number): Observable<Empresa> {
        return this.httpClient.get<Empresa>(`/api/Empresa/SelecionarEmpresa/${idEmpresa}`);
    }
    Salvar(empresa: Empresa) {
        console.log(empresa);
        
        return this.httpClient.post(`/api/Empresa/InsertOrUpdate`, empresa).subscribe(result => {
            console.log(result);

        }, (error: any) => { console.log(error) });
    }
   
    Deletar(id:Number):Observable<any>{
        return this.httpClient.delete<any>(`/api/Empresa/Delete/${id}`);
    }
   

}