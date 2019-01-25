import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { PlantIndexModel } from '../models/plant.index.model';
import { PlantsUrlProvider } from './plantsurlprovider';
import { PlantDetailsModel } from '../models/plant.details';
import { PlantStateModel } from '../models/plantstate.model';
import { PlantEventTypeModel } from '../models/planteventtype.model';

@Injectable()
export class PlantsService {
    constructor(private urls: PlantsUrlProvider,
        private http: HttpClient) {
    }

    mapState(state: PlantStateModel): string {
        switch (state) {
            case PlantStateModel.Healthy: return 'Healthy';
            case PlantStateModel.Flowering: return 'Flowering';
            case PlantStateModel.Fruiting: return 'Fruiting';
            case PlantStateModel.Harvested: return 'Harvested';
            case PlantStateModel.Sick: return 'Sick';
            case PlantStateModel.Deceased: return 'Deceased';
            case PlantStateModel.Sprouting: return 'Sprouting';
            default: return state;
        }
    }

    mapEventType(eventType: PlantEventTypeModel): string {
        switch (eventType) {
            case PlantEventTypeModel.Insects: return 'Insects';
            case PlantEventTypeModel.Pests: return 'Pests';
            case PlantEventTypeModel.Fungi: return 'Fungi';
            case PlantEventTypeModel.Disease: return 'Disease';
            case PlantEventTypeModel.UnderWatering: return 'Underwatering';
            case PlantEventTypeModel.OverWatering: return 'Overwatering';
            case PlantEventTypeModel.Moving: return 'Moving';
            case PlantEventTypeModel.Growth: return 'Growth';
            case PlantEventTypeModel.StartedTracking: return 'Started tracking';
            case PlantEventTypeModel.Nutrition: return 'Nutrition';
            case PlantEventTypeModel.Sprouting: return 'Sprouting';
        }
    }

    getAll(): Observable<PlantIndexModel[]> {
        return this.http.get<PlantIndexModel[]>(this.urls.IndexUrl());
    }

    get(id: string): Observable<PlantDetailsModel> {
        return this.http.get<PlantDetailsModel>(this.urls.DetailsUrl(id));
    }

    delete(id: string): Observable<Object> {
        return this.http.delete(this.urls.DeleteUrl(id), { observe: 'response' });
    }

    create(value: any): Observable<Object> {
        return this.http.post(this.urls.CreateUrl(), value, { observe: 'response' });
    }

    edit(id: string, value: any): Observable<Object> {
        return this.http.put(this.urls.EditUrl(id), value, { observe: 'response' });
    }
}
