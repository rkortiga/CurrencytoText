import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Environment } from '../../environment/environment';
import { Currency } from '../../model/currency';

@Injectable({
    providedIn: 'root'
})
export class CurrencyService {

    constructor(private http: HttpClient) { }

    convert(obj: Currency) : Observable<Currency> {
        return this.http.post<Currency>(`${Environment.apiUrl}/api/Currency/ConvertToText`, obj);
    }
}