import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { FieldService } from '../services/fields.service';
import { FieldIndexModel } from '../models/field.index.model';
import { FieldsUrlProvider } from '../services/fieldsurlprovider';

@Component({
  selector: 'app-field-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class FieldIndexComponent implements OnInit, OnDestroy {
  fields: FieldIndexModel[];
  fieldsSubscription: Subscription;

  constructor(private service: FieldService) { }

  ngOnInit() {
    this.fieldsSubscription = this.service.getAll().subscribe(
      (fields: FieldIndexModel[]) => {
        this.fields = fields;
      });
  }

  ngOnDestroy() {
    this.fieldsSubscription.unsubscribe();
  }
}
