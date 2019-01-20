import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { FieldsUrlProvider } from './fieldsurlprovider';
import { FieldIndexModel } from '../models/field.index.model';
import { FieldDetailsModel } from '../models/field.details.model';

@Injectable()
export class FieldService {
    constructor(private urls: FieldsUrlProvider,
        private httpClient: HttpClient) {
    }

    getAll(): Observable<FieldIndexModel[]> {
        return this.httpClient.get<FieldIndexModel[]>(this.urls.IndexUrl());
    }

    get(id: string): Observable<FieldDetailsModel> {
        return this.httpClient.get<FieldDetailsModel>(this.urls.DetailsUrl(id));
    }

    create(value: any): Observable<Object> {
        return this.httpClient.post(this.urls.CreateUrl(), value, { observe: 'response' });
    }
}
