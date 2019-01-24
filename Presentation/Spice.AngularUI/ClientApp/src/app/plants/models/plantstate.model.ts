export enum PlantStateModel {
    Healthy,
    Flowering,
    Fruiting,
    Harvested,
    Sick,
    Deceased,
    Sprouting
}

export namespace PlantStateModel {

    export function values() {
      return Object.keys(PlantStateModel).filter(
        (type) => isNaN(<any>type) && type !== 'values'
      );
    }
  }
