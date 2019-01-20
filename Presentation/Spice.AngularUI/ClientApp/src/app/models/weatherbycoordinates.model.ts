import { WeatherInfo } from './weatherinfo.model';

export class WeatherByCoordinates {
    latitude: number;
    longitude: number;
    date: Date;
    info: WeatherInfo;
}
