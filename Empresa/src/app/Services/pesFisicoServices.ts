import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { PesFisica } from '../Model/pesFisica';

@Injectable({
    providedIn: 'root'
})
export class PesFisicaService {

    constructor(private httpClient: HttpClient) { }
  
    SelecionarPesFisicas(idPesFisica: number): Observable<PesFisica[]> {
        return this.httpClient.get<PesFisica[]>(`/api/PesFisica/SelecionarPesFisicas/${idPesFisica}`);
    }
    Salvar(fisica: PesFisica):Observable<PesFisica> {      
            return this.httpClient.post<PesFisica>(`/api/PesFisica/InsertOrUpdate`, fisica);
    }
    Deletar(id:Number):Observable<any>{
        return this.httpClient.delete<any>(`/api/PesFisica/Delete/${id}`);
    }  
}