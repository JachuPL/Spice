import { SpeciesIndexModel } from '../../species/models/species.index.model';
import { FieldIndexModel } from '../../fields/models/field.index.model';
import { AdministeredNutrientIndexModel } from '../../nutrients/models/administerednutrient.index.model';
import { PlantEventIndexViewModel } from './plantevents.index.model';

export class PlantDetailsModel {
    id: string;
    name: string;
    species: SpeciesIndexModel;
    field: FieldIndexModel;
    row: number;
    column: number;
    planted: Date;
    state: number;
    nutrients: AdministeredNutrientIndexModel[];
    events: PlantEventIndexViewModel[];
}
