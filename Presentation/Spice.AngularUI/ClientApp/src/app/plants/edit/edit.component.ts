import { Component, OnInit, OnDestroy } from '@angular/core';
import { PlantStateModel } from '../models/plantstate.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { formatDate } from '@angular/common';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { FieldIndexModel } from '../../fields/models/field.index.model';
import { SpeciesIndexModel } from '../../species/models/species.index.model';
import { PlantsService } from '../services/plants.service';
import { FieldService } from '../../fields/services/fields.service';
import { SpeciesService } from '../../species/services/species.service';
import { PlantDetailsModel } from '../models/plant.details';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class PlantEditComponent implements OnInit, OnDestroy {
  keys = Object.keys;
  states = PlantStateModel;
  plantEditionForm: FormGroup;
  private fieldsSubscription: Subscription;
  private speciesSubscription: Subscription;
  errors: {}[];
  fields: FieldIndexModel[];
  species: SpeciesIndexModel[];
  plantSubscription: Subscription;
  id: string;
  constructor(private plantsService: PlantsService,
    private fieldsService: FieldService,
    private speciesService: SpeciesService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.plantEditionForm = new FormGroup({
      'name': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'fieldId': new FormControl('', [Validators.required]),
      'speciesId': new FormControl('', [Validators.required]),
      'state': new FormControl('', [Validators.required]),
      'row': new FormControl('1', [Validators.required, Validators.min(1)]),
      'column': new FormControl('1', [Validators.required, Validators.min(1)]),
      'planted': new FormControl(new Date(), [Validators.required])
    });
    this.activatedRoute.params.subscribe((param: Params) => {
      this.id = param['id'];
      this.plantSubscription = this.plantsService.get(this.id).subscribe(((plant: PlantDetailsModel) => {
        delete plant['id'];
        delete plant['nutrients'];
        delete plant['events'];
        plant['fieldId'] = plant['field'].id;
        delete plant['field'];
        plant['speciesId'] = plant['species'].id;
        delete plant['species'];
        this.plantEditionForm.setValue(plant);  // TODO: fix - field appears as blank after initialization
        this.fieldsSubscription = this.fieldsService.getAll().subscribe((values: FieldIndexModel[]) => {
          this.fields = values;
        });
        this.speciesSubscription = this.speciesService.getAll().subscribe((values: SpeciesIndexModel[]) => {
          this.species = values;
        });
      }),
      (error) => {
        console.error(error);
        this.router.navigate(['/fields']);
      });
    });
  }

  ngOnDestroy() {
    this.fieldsSubscription.unsubscribe();
    this.speciesSubscription.unsubscribe();
    this.plantSubscription.unsubscribe();
  }

  get f() {
    return this.plantEditionForm;
  }

  onSubmit() {
    this.plantEditionForm.value['planted'] = formatDate(this.plantEditionForm.value['planted'], 'yyyy-MM-dd HH:mm:ss', 'en');
    this.plantsService.create(this.plantEditionForm.value).subscribe(
      (value: HttpResponse<object>) => this.router.navigate(['/plants', value.body]), // TODO: make index list refresh on success
      (error: HttpErrorResponse) => {
        switch (error.status) {
          case 400:
            const errors = error.error.errors;
            if (errors) {
              this.errors = Object.values(errors);
            }
            break;
          case 409:
            this.errors = [[error.error.error]];
            break;
        }
      });
  }

}
