import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { UrlProvider } from './services/urlprovider';
import { FieldsUrlProvider } from './fields/services/fieldsurlprovider';
import { FieldIndexComponent } from './fields/index/index.component';
import { FieldService } from './fields/services/fields.service';
import { FieldDetailsComponent } from './fields/details/details.component';
import { WeatherService } from './services/weather.service';
import { FieldCreateComponent } from './fields/create/create.component';
import { FieldEditComponent } from './fields/edit/edit.component';
import { SpeciesIndexComponent } from './species/index/index.component';
import { SpeciesService } from './species/services/species.service';
import { SpeciesUrlProvider } from './species/services/speciesurlprovider';
import { SpeciesDetailsComponent } from './species/details/details.component';
import { SpeciesCreateComponent } from './species/create/create.component';
import { SpeciesEditComponent } from './species/edit/edit.component';
import { PlantsIndexComponent } from './plants/index/index.component';
import { PlantsService } from './plants/services/plants.service';
import { PlantsUrlProvider } from './plants/services/plantsurlprovider';
import { PlantDetailsComponent } from './plants/details/details.component';
import { PlantCreateComponent } from './plants/create/create.component';
import { PlantEditComponent } from './plants/edit/edit.component';
import { AngularDateTimePickerModule } from 'angular2-datetimepicker';
import { PlantListComponent } from './plants/list/list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FieldIndexComponent,
    FieldDetailsComponent,
    FieldCreateComponent,
    FieldEditComponent,
    SpeciesIndexComponent,
    SpeciesDetailsComponent,
    SpeciesCreateComponent,
    SpeciesEditComponent,
    PlantsIndexComponent,
    PlantDetailsComponent,
    PlantCreateComponent,
    PlantEditComponent,
    PlantListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fields', component: FieldIndexComponent, children: [
        { path: 'new', component: FieldCreateComponent },
        { path: ':id', component: FieldDetailsComponent },
        { path: ':id/edit', component: FieldEditComponent }
      ] },
      { path: 'species', component: SpeciesIndexComponent, children: [
        { path: 'new', component: SpeciesCreateComponent },
        { path: ':id', component: SpeciesDetailsComponent },
        { path: ':id/edit', component: SpeciesEditComponent }
      ] },
      { path: 'plants', component: PlantsIndexComponent, children: [
        { path: 'new', component: PlantCreateComponent },
        { path: ':id', component: PlantDetailsComponent },
        { path: ':id/edit', component: PlantEditComponent }
      ] }
    ]),
    ReactiveFormsModule,
    AngularDateTimePickerModule
  ],
  providers: [UrlProvider,
    FieldsUrlProvider, FieldService, WeatherService,
    SpeciesService, SpeciesUrlProvider,
    PlantsService, PlantsUrlProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
