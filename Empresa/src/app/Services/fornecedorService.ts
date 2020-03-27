import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Fornecedor } from '../Model/fornecedor';
import { FornecedorView } from '../Model/fornecedorView';

@Injectable({
    providedIn: 'root'
})
export class FornecedorService {

    constructor(private httpClient: HttpClient) { }
  
    SelecionarFornecedores(): Observable<FornecedorView[]> {
        return this.httpClient.get<FornecedorView[]>(`/api/Fornecedor/SelecionarFornecedores`);
    }
    Salvar(fornecedor: Fornecedor):Observable<Fornecedor> {      
            return this.httpClient.post<Fornecedor>(`/api/Fornecedor/InsertOrUpdate`, fornecedor);
    }
    Deletar(id:Number):Observable<any>{
        return this.httpClient.delete<any>(`/api/Fornecedor/Delete/${id}`);
    }  
}