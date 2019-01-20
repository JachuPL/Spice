import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, ReplaySubject } from 'rxjs';

import { WeatherByCoordinates } from '../models/weatherbycoordinates.model';
import { UrlProvider } from './urlprovider';
import { WeatherInfo } from '../models/weatherinfo.model';

@Injectable()
export class WeatherService {
    private weatherSubject = new ReplaySubject<WeatherByCoordinates>(1);
    weather: Observable<WeatherByCoordinates> = this.weatherSubject.asObservable();
    constructor(private httpClient: HttpClient,
        private urls: UrlProvider) {
    }

    private weatherByCoordinates: WeatherByCoordinates[] = [];

    getWeatherByCoordinates(latitude: number, longitude: number) {
        const existingRecords = this.getRecordsSortedByDate(latitude, longitude);
        if (existingRecords.length === 0) {
            this.httpClient.get(this.urls.WeatherApiWithKeyAddress + '&lat=' + latitude + '&lon=' + longitude)
                .subscribe((value) => {
                    const newWeatherRecord = this.createNewWeatherRecord(value);
                    this.weatherByCoordinates.push(newWeatherRecord);
                    this.weatherSubject.next(newWeatherRecord);
                });
        } else {
            this.weatherSubject.next(existingRecords[0]);
        }
    }

    private createNewWeatherRecord(value: Object): any {
        const newWeatherRecord = new WeatherByCoordinates;
        newWeatherRecord.date = new Date();
        newWeatherRecord.latitude = value['coord'].lat;
        newWeatherRecord.longitude = value['coord'].lon;
        newWeatherRecord.info = this.createNewWeatherInfoFromValue(value);
        return newWeatherRecord;
    }

    private createNewWeatherInfoFromValue(value: Object): WeatherInfo {
        console.log(value);
        const weatherInfo = new WeatherInfo();
        weatherInfo.date = new Date(value['dt'] * 1000);

        const coordinates = value['coord'];
        weatherInfo.coordinates = {
            latitude: coordinates.lat,
            longitude: coordinates.lon
        };

        const weatherConditionsArray =  Object.values(value['weather']);
        weatherInfo.weather = [];
        for (let i = 0; i < weatherConditionsArray.length; i++) {
            weatherInfo.weather.push({
                id: weatherConditionsArray[i]['id'],
                main: weatherConditionsArray[i]['main'],
                description: weatherConditionsArray[i]['description'],
                icon: weatherConditionsArray[i]['icon']
            });
        }

        const main = value['main'];
        weatherInfo.general = {
            temperature: main.temp,
            maximumTemperatureDeviation: main.temp_max,
            minimumTemperatureDeviation: main.temp_min,
            humidity: main.humidity,
            pressure: main.pressure,
            pressureAtGroundLevel: main.grnd_level,
            pressureAtSeaLevel: main.sea_level
        };

        const wind = value['wind'];
        weatherInfo.wind = {
            degrees: wind.deg,
            speed: wind.speed
        };

        weatherInfo.clouds = value['clouds'].all;

        const rain = value['rain'];
        weatherInfo.rain = {
            volumeOfLast1h: rain === undefined ? 0 : rain['1h'],
            volumeOfLast3h: rain === undefined ? 0 : rain['3h']
        };

        const snow = value['snow'];
        weatherInfo.snow = {
            volumeOfLast1h: snow === undefined ? 0 : snow['1h'],
            volumeOfLast3h: snow === undefined ? 0 : snow['3h'],
        };

        const sys = value['sys'];
        weatherInfo.sunrise = new Date(sys.sunrise * 1000);
        weatherInfo.sunset = new Date(sys.sunset * 1000);

        return weatherInfo;
    }

    private getRecordsSortedByDate(latitude: number, longitude: number) {
        return this.weatherByCoordinates
            .sort((a, b) => a.date.getTime() - b.date.getTime())
            .filter(this.weatherRecordsByCoordinatesAndUpdateDate(latitude, longitude));
    }

    private weatherRecordsByCoordinatesAndUpdateDate(latitude: number, longitude: number) {
        return (value: WeatherByCoordinates) => {
            const date = new Date();
            const lastDatePlus10Minutes = new Date(value.date.getTime() + 10 * 60000);
            if (value.latitude === latitude && value.longitude === longitude && lastDatePlus10Minutes < date) {
                return value;
            }
        };
    }
}
