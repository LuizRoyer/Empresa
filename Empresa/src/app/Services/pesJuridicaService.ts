import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { PesJuridico } from '../Model/pesJuridico';

@Injectable({
    providedIn: 'root'
})
export class PesJuridicaService {

    constructor(private httpClient: HttpClient) { }
  
    SelecionarPesJuridicas(idPesJuridica: number): Observable<PesJuridico[]> {
        return this.httpClient.get<PesJuridico[]>(`/api/PesJuridica/SelecionarPesJuridicas/${idPesJuridica}`);
    }
    Salvar(juridica: PesJuridico):Observable<PesJuridico> {      
            return this.httpClient.post<PesJuridico>(`/api/PesJuridica/InsertOrUpdate`, juridica);
    }
    Deletar(id:Number):Observable<any>{
        return this.httpClient.delete<any>(`/api/PesJuridica/Delete/${id}`);
    }  
}