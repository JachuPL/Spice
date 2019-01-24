import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { SpeciesService } from '../services/species.service';
import { SpeciesDetailsModel } from '../models/species.details.model';
import { SpeciesNutritionSummaryModel } from '../models/speciesnutritionsummary.model';

@Component({
  selector: 'app-species-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class SpeciesDetailsComponent implements OnInit, OnDestroy {
  idSubscription: Subscription;
  speciesSubscription: Subscription;
  species: SpeciesDetailsModel;
  deletionSubscription: Subscription;
  nutrientSummarySubscription: Subscription;
  nutritionSummary: SpeciesNutritionSummaryModel[];
  constructor(private service: SpeciesService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.idSubscription = this.activatedRoute.params.subscribe((params: Params) => {
      const id = params['id'];
      this.speciesSubscription = this.service.get(id).subscribe(
        (species: SpeciesDetailsModel) => {
          this.species = species;
          this.nutrientSummarySubscription = this.service.getSummary(id).subscribe((value: SpeciesNutritionSummaryModel[]) => {
            this.nutritionSummary = value;
          });
        },
        (error) => {
          console.error(error);
          this.router.navigate(['/species']);
        });
    });
  }

  ngOnDestroy() {
    if (this.idSubscription) {
      this.idSubscription.unsubscribe();
    }

    if (this.speciesSubscription) {
      this.speciesSubscription.unsubscribe();
    }

    if (this.nutrientSummarySubscription) {
      this.nutrientSummarySubscription.unsubscribe();
    }
  }

  onDelete() {
    this.deletionSubscription = this.service.delete(this.species.id).subscribe((result: HttpResponse<object>) => {
      if (result.status === 204) {
        this.router.navigate(['/species']);
      }
    });
  }


}
