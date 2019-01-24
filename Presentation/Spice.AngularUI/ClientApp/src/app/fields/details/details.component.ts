import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { FieldDetailsModel } from '../models/field.details.model';
import { FieldService } from '../services/fields.service';
import { WeatherService } from '../../services/weather.service';
import { WeatherByCoordinates } from '../../models/weatherbycoordinates.model';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-field-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class FieldDetailsComponent implements OnInit, OnDestroy {
  idSubscription: Subscription;
  fieldSubscription: Subscription;
  field: FieldDetailsModel;
  weatherSubscription: Subscription;
  weather: WeatherByCoordinates;
  deletionSubscription: Subscription;
  constructor(private service: FieldService,
    private weatherService: WeatherService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.idSubscription = this.activatedRoute.params.subscribe((params: Params) => {
      const id = params['id'];
      this.fieldSubscription = this.service.get(id).subscribe(
        (field: FieldDetailsModel) => {
          this.field = field;
          this.weatherService.getWeatherByCoordinates(this.field.latitude, this.field.longtitude);
          this.weatherSubscription = this.weatherService.weather.subscribe((value) => {
            this.weather = value;
            console.log(this.weather);
          });
        },
        (error) => {
          console.error(error);
          this.router.navigate(['/fields']);
        });
    });
  }

  ngOnDestroy() {
    if (this.idSubscription) {
      this.idSubscription.unsubscribe();
    }

    if (this.fieldSubscription) {
    this.fieldSubscription.unsubscribe();
    }

    if (this.weatherSubscription) {
    this.weatherSubscription.unsubscribe();
    }
  }

  isNightAtLocation() {
    return this.weather.info.sunset < this.weather.info.date;
  }

  onDelete() {
    this.deletionSubscription = this.service.delete(this.field.id).subscribe((result: HttpResponse<object>) => {
      if (result.status === 204) {
        this.router.navigate(['/fields']);
      }
    });
  }

}
