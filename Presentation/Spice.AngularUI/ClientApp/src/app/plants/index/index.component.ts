import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { PlantIndexModel } from '../models/plant.index.model';
import { PlantsService } from '../services/plants.service';

@Component({
  selector: 'app-plants-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class PlantsIndexComponent implements OnInit, OnDestroy {

  plants: PlantIndexModel[];
  plantsSubscription: Subscription;

  constructor(private service: PlantsService) { }

  ngOnInit() {
    this.plantsSubscription = this.service.getAll().subscribe(
      (plants: PlantIndexModel[]) => {
        this.plants = plants;
      });
  }

  ngOnDestroy() {
    this.plantsSubscription.unsubscribe();
  }
}
