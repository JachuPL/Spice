import { Component, OnInit } from '@angular/core';
import { SpeciesService } from '../services/species.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-species-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class SpeciesCreateComponent implements OnInit {
  speciesCreationForm: FormGroup;
  errors: {}[];
  constructor(private speciesService: SpeciesService,
    private router: Router) { }

  ngOnInit() {
    this.speciesCreationForm = new FormGroup({
      'name': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'latinName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      'description': new FormControl('', [this.validDescription.bind(this)])
    });
  }

  get f() {
    return this.speciesCreationForm;
  }

  onSubmit() {
    this.speciesService.create(this.speciesCreationForm.value).subscribe(
      (value: HttpResponse<object>) => this.router.navigate(['/species', value.body]), // TODO: make index list refresh on success
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
