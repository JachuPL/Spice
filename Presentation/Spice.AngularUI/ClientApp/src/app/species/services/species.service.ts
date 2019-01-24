import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SpeciesIndexModel } from '../models/species.index.model';
import { SpeciesUrlProvider } from './speciesurlprovider';

@Injectable()
export class SpeciesService {
    constructor(private http: HttpClient,
        private urls: SpeciesUrlProvider) {
    }
  getAll(): Observable<SpeciesIndexModel[]> {
      return this.http.get<SpeciesIndexModel[]>(this.urls.IndexUrl());
  }
}
