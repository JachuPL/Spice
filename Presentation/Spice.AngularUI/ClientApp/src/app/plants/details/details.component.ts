import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { PlantDetailsModel } from '../models/plant.details';
import { PlantsService } from '../services/plants.service';

@Component({
  selector: 'app-plant-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class PlantDetailsComponent implements OnInit, OnDestroy {
  idSubscription: Subscription;
  plantSubscription: Subscription;
  plant: PlantDetailsModel;
  deletionSubscription: Subscription;
  constructor(private service: PlantsService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.idSubscription = this.activatedRoute.params.subscribe((params: Params) => {
      const id = params['id'];
      this.plantSubscription = this.service.get(id).subscribe(
        (plant: PlantDetailsModel) => {
          this.plant = plant;
        },
        (error) => {
          console.error(error);
          this.router.navigate(['/plants']);
        });
    });
  }

  ngOnDestroy() {
    if (this.idSubscription) {
      this.idSubscription.unsubscribe();
    }

    if (this.plantSubscription) {
      this.plantSubscription.unsubscribe();
    }
  }

  onDelete() {
    this.deletionSubscription = this.service.delete(this.plant.id).subscribe((result: HttpResponse<object>) => {
      if (result.status === 204) {
        this.router.navigate(['/plants']);
      }
    });
  }
}
