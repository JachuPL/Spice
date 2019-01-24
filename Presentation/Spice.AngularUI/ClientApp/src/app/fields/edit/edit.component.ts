import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { FieldService } from '../services/fields.service';
import { Subscription } from 'rxjs';
import { FieldDetailsModel } from '../models/field.details.model';

@Component({
  selector: 'app-field-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class FieldEditComponent implements OnInit, OnDestroy {
  fieldEditionForm: FormGroup;
  errors: {}[];
  fieldSubscription: Subscription;
  id: string;
  constructor(private fieldService: FieldService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.fieldEditionForm = new FormGroup({
      'name': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'description': new FormControl('', [this.validDescription.bind(this)]),
      'latitude': new FormControl('', [Validators.required, this.validLatitudeRange.bind(this)]),
      'longtitude': new FormControl('', [Validators.required, this.validLongitudeRange.bind(this)])
    });
    this.activatedRoute.params.subscribe((param: Params) => {
      this.id = param['id'];
      this.fieldSubscription = this.fieldService.get(this.id).subscribe(((field: FieldDetailsModel) => {
        delete field['id'];
        delete field['plants'];
        this.fieldEditionForm.setValue(field);
      }),
      (error) => {
        console.error(error);
        this.router.navigate(['/fields']);
      });
    });
  }

  ngOnDestroy() {
    this.fieldSubscription.unsubscribe();
  }

  get f() {
    return this.fieldEditionForm;
  }

  onSubmit() {
    this.fieldService.edit(this.id, this.fieldEditionForm.value).subscribe(
      (value: HttpResponse<object>) => this.router.navigate(['/fields', this.id]), // TODO: make index list refresh on success
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

  validLatitudeRange(control: FormControl): {[s: string]: boolean} {
    if (control.value < -90 || control.value > 90) {
      return {'invalidLatitude': true};
    }
    return null;
  }

  validLongitudeRange(control: FormControl): {[s: string]: boolean} {
    if (control.value < -180 || control.value > 180) {
      return {'invalidLongitude': true};
    }
    return null;
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
