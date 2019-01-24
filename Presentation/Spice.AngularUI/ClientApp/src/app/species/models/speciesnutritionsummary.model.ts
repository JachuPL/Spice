import { NutrientDetailsModel } from '../../nutrients/models/nutrient.details.model';

export class SpeciesNutritionSummaryModel {
    nutrient: NutrientDetailsModel;
    totalAmount: number;
    firstAdministration: Date;
    lastAdministration: Date;
}