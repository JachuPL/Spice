import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { SpeciesService } from '../services/species.service';
import { SpeciesDetailsModel } from '../models/species.details.model';

@Component({
  selector: 'app-species-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class SpeciesEditComponent implements OnInit, OnDestroy {
  speciesEditionForm: FormGroup;
  errors: {}[];
  speciesSubscription: Subscription;
  id: string;
  constructor(private speciesService: SpeciesService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.speciesEditionForm = new FormGroup({
      'name': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'latinName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'description': new FormControl('', [this.validDescription.bind(this)])
    });
    this.activatedRoute.params.subscribe((param: Params) => {
      this.id = param['id'];
      this.speciesSubscription = this.speciesService.get(this.id).subscribe(((species: SpeciesDetailsModel) => {
        delete species['id'];
        delete species['plants'];
        this.speciesEditionForm.setValue(species);
      }),
      (error) => {
        console.error(error);
        this.router.navigate(['/species']);
      });
    });
  }

  ngOnDestroy() {
    this.speciesSubscription.unsubscribe();
  }

  get f() {
    return this.speciesEditionForm;
  }

  onSubmit() {
    this.speciesService.edit(this.id, this.speciesEditionForm.value).subscribe(
      (value: HttpResponse<object>) => this.router.navigate(['/species', this.id]), // TODO: make index list refresh on success
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

  validDescription(control: FormControl): {[s: string]: boolean} {
    const descriptionLength = control.value.trim().length;
    if (descriptionLength !== 0
      && (descriptionLength < 5 || descriptionLength > 500)) {
      return {'invalidDescriptionLength': true};
    }

    return null;
  }

}
