import { Injectable, Output } from '@angular/core';

@Injectable()
export class UrlProvider {
    @Output() readonly BaseAddress: string = 'https://192.168.0.49';
    @Output() readonly BaseApiAddress: string = this.BaseAddress + '/api';

    @Output() readonly BaseWeatherApiAddress: string = 'http://api.openweathermap.org/data/2.5/weather';
    @Output() readonly WeatherApiWithKeyAddress: string = this.BaseWeatherApiAddress
        + '?appid=a1b641ebabe8b02c21a73a42de1f8255&units=metric&lang=pl';
}
