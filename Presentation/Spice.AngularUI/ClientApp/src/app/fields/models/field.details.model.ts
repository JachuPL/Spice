import { PlantIndexModel } from 'src/app/plants/models/plant.index.model';

export class FieldDetailsModel {
    id: string;
    name: string;
    description: string;
    latitude: number;
    longtitude: number;
    plants: PlantIndexModel[];
}
