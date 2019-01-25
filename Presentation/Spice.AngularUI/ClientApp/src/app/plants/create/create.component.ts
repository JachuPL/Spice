import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { PlantsService } from '../services/plants.service';
import { FieldService } from '../../fields/services/fields.service';
import { SpeciesService } from '../../species/services/species.service';
import { FieldIndexModel } from '../../fields/models/field.index.model';
import { SpeciesIndexModel } from '../../species/models/species.index.model';
import { formatDate } from '@angular/common';
import { PlantStateModel } from '../models/plantstate.model';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class PlantCreateComponent implements OnInit, OnDestroy {
  datePickerSettings = {
    bigBanner: true,
    format: 'dd.MM.yyyy'
  };

  keys = Object.keys;
  states = PlantStateModel;
  plantCreationForm: FormGroup;
  private fieldsSubscription: Subscription;
  private speciesSubscription: Subscription;
  errors: {}[];
  fields: FieldIndexModel[];
  species: SpeciesIndexModel[];
  constructor(private plantsService: PlantsService,
    private fieldsService: FieldService,
    private speciesService: SpeciesService,
    private router: Router) { }

  ngOnInit() {
    this.plantCreationForm = new FormGroup({
      'name': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'fieldId': new FormControl('', [Validators.required]),
      'speciesId': new FormControl('', [Validators.required]),
      'state': new FormControl('', [Validators.required]),
      'row': new FormControl('1', [Validators.required, Validators.min(1)]),
      'column': new FormControl('1', [Validators.required, Validators.min(1)]),
      'planted': new FormControl(new Date(), [Validators.required])
    });
    this.fieldsSubscription = this.fieldsService.getAll().subscribe((values: FieldIndexModel[]) => {
      this.fields = values;
    });
    this.speciesSubscription = this.speciesService.getAll().subscribe((values: SpeciesIndexModel[]) => {
      this.species = values;
    });
  }

  ngOnDestroy() {
    this.fieldsSubscription.unsubscribe();
    this.speciesSubscription.unsubscribe();
  }

  get f() {
    return this.plantCreationForm;
  }

  onSubmit() {
    this.plantCreationForm.value['planted'] = formatDate(this.plantCreationForm.value['planted'], 'yyyy-MM-dd HH:mm:ss', 'en');
    this.plantsService.create(this.plantCreationForm.value).subscribe(
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
