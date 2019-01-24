import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { SpeciesIndexModel } from '../models/species.index.model';
import { SpeciesService } from '../services/species.service';

@Component({
  selector: 'app-species-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class SpeciesIndexComponent implements OnInit, OnDestroy {

  species: SpeciesIndexModel[];
  speciesSubscription: Subscription;

  constructor(private service: SpeciesService) { }

  ngOnInit() {
    this.speciesSubscription = this.service.getAll().subscribe(
      (species: SpeciesIndexModel[]) => {
        this.species = species;
      });
  }

  ngOnDestroy() {
    this.speciesSubscription.unsubscribe();
  }
}
