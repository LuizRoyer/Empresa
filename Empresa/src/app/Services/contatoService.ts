import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contato } from '../Model/contato';

@Injectable({
    providedIn: 'root'
})
export class ContatoService {

    constructor(private httpClient: HttpClient) { }

    SelecionarContato(id: number): Observable<Contato[]> {
        return this.httpClient.get<Contato[]>(`/api/Contato/SelecionarContatos/${id}`);
    }
    Salvar(contato: Contato): Observable<Contato> {
        return this.httpClient.post<Contato>(`/api/Contato/InsertOrUpdate`, contato);
    }
    Deletar(id: Number, idPessoa: number): Observable<any> {
        return this.httpClient.delete<any>(`/api/Contato/Delete/${id}/${idPessoa}`);
    }
}