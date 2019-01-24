import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SpeciesIndexModel } from '../models/species.index.model';
import { SpeciesUrlProvider } from './speciesurlprovider';
import { SpeciesDetailsModel } from '../models/species.details.model';
import { SpeciesNutritionSummaryModel } from '../models/speciesnutritionsummary.model';

@Injectable()
export class SpeciesService {
    constructor(private http: HttpClient,
        private urls: SpeciesUrlProvider) {
    }

    getAll(): Observable<SpeciesIndexModel[]> {
      return this.http.get<SpeciesIndexModel[]>(this.urls.IndexUrl());
    }

    get(id: string): Observable<SpeciesDetailsModel> {
        return this.http.get<SpeciesDetailsModel>(this.urls.DetailsUrl(id));
    }

    getSummary(id: string): Observable<SpeciesNutritionSummaryModel[]> {
        return this.http.get<SpeciesNutritionSummaryModel[]>(this.urls.SummaryUrl(id));
    }

    delete(id: string): Observable<Object> {
        return this.http.delete(this.urls.DeleteUrl(id), {observe: 'response'});
    }

    create(value: any): Observable<Object> {
        return this.http.post(this.urls.CreateUrl(), value, { observe: 'response' });
    }

    edit(id: string, value: any): Observable<Object> {
        return this.http.put(this.urls.EditUrl(id), value, { observe: 'response' });
    }
}
