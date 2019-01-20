import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';

import { FieldDetailsModel } from '../models/field.details.model';
import { FieldService } from '../services/fields.service';
import { WeatherService } from '../../services/weather.service';
import { WeatherByCoordinates } from '../../models/weatherbycoordinates.model';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class FieldDetailsComponent implements OnInit, OnDestroy {
  idSubscription: Subscription;
  fieldSubscription: Subscription;
  field: FieldDetailsModel;
  weatherSubscription: Subscription;
  weather: WeatherByCoordinates;
  constructor(private service: FieldService,
    private weatherService: WeatherService,
    private activatedRoute: ActivatedRoute) { }

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
        });
    });
  }

  ngOnDestroy() {
    this.idSubscription.unsubscribe();
    this.fieldSubscription.unsubscribe();
    this.weatherSubscription.unsubscribe();
  }

  isNightAtLocation() {
    return this.weather.info.sunset < this.weather.info.date;
  }

}
