import { PlantIndexModel } from '../../plants/models/plant.index.model';

export class SpeciesDetailsModel {
    id: string;
    name: string;
    latinName: string;
    description: string;
    plants: PlantIndexModel[];
}
