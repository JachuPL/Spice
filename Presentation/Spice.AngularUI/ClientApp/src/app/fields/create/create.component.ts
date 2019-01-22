import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { FieldService } from '../services/fields.service';

@Component({
  selector: 'app-field-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class FieldCreateComponent implements OnInit {
  fieldCreationForm: FormGroup;
  errors: {}[];
  constructor(private fieldService: FieldService,
    private router: Router) { }

  ngOnInit() {
    this.fieldCreationForm = new FormGroup({
      'name': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'description': new FormControl('', [this.validDescription.bind(this)]),
      'latitude': new FormControl('0', [Validators.required, this.validLatitudeRange.bind(this)]),
      'longtitude': new FormControl('0', [Validators.required, this.validLongitudeRange.bind(this)])
    });
  }

  get f() {
    return this.fieldCreationForm;
  }

  onSubmit() {
    this.fieldService.create(this.fieldCreationForm.value).subscribe(
      (value: HttpResponse<object>) => this.router.navigate(['/fields', value.body]), // TODO: make index list refresh on success
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
