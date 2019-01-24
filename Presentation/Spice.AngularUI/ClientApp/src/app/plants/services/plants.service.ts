import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { PlantIndexModel } from '../models/plant.index.model';
import { PlantsUrlProvider } from './plantsurlprovider';

@Injectable()
export class PlantsService {
    constructor(private urls: PlantsUrlProvider,
        private http: HttpClient) {
    }

    getAll(): Observable<PlantIndexModel[]> {
        return this.http.get<PlantIndexModel[]>(this.urls.IndexUrl());
    }
}
