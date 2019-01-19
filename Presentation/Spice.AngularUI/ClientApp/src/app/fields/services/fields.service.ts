import { Injectable } from '@angular/core';
import { FieldsUrlProvider } from './fieldsurlprovider';
import { FieldIndexModel } from '../models/field.index.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class FieldService {
    constructor(private urls: FieldsUrlProvider,
        private httpClient: HttpClient) {
    }

    getAll(): Observable<FieldIndexModel[]> {
        return this.httpClient.get<FieldIndexModel[]>(this.urls.IndexUrl());
    }
}
