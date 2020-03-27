import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Endereco } from '../Model/endereco';
import { Observable } from 'rxjs';
import { Estado } from '../Model/estado';

@Injectable({
    providedIn: 'root'
})
export class EnderecoService {

    constructor(private httpClient: HttpClient) { }

    SelecionarEnderecos() {
        return this.httpClient.get<Endereco[]>(`/api/Endereco/SelecionarEnderecos`);
    }

    BuscarCep(cep: string) {
        return this.httpClient.get<Endereco>(`http://viacep.com.br/ws/${cep}/json/`);
    }
    SelecionarEndereco(idEndereco: number): Observable<Endereco> {
        return this.httpClient.get<Endereco>(`/api/Endereco/SelecionarEndereco/${idEndereco}`);
    }
    Salvar(endereco: Endereco): Observable<any>  {        
        return this.httpClient.post(`/api/Endereco/InsertOrUpdate`, endereco);
    }
   
    Deletar(id:Number):Observable<any>{
        return this.httpClient.delete<any>(`/api/Endereco/Delete/${id}`);
    }

    SelecionarEstados() {
        return this.httpClient.get<Estado[]>(`/api/Estado/SelecionarEstados`);
    }

}