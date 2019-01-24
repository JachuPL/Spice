import { SnowConditions } from './snowconditions.model';
import { RainConditions } from './rainconditions.model';
import { WindConditions } from './windconditions.model';
import { GeneralConditions } from './generalconditions.model';
import { WeatherConditions } from './weatherconditions.model';
import { Coordinates } from './coordinates.model';

export class WeatherInfo {
    coordinates: Coordinates;
    sunrise: Date;
    sunset: Date;
    weather: WeatherConditions[];
    general: GeneralConditions;
    wind: WindConditions;
    rain: RainConditions;
    snow: SnowConditions;
    clouds: number;
    date: Date;
}
