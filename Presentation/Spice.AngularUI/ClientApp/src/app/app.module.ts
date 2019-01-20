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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FieldIndexComponent,
    FieldDetailsComponent,
    FieldCreateComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fields', component: FieldIndexComponent, children: [
        { path: 'new', component: FieldCreateComponent },
        { path: ':id', component: FieldDetailsComponent }
      ] },
    ]),
    ReactiveFormsModule
  ],
  providers: [UrlProvider, FieldsUrlProvider, FieldService, WeatherService],
  bootstrap: [AppComponent]
})
export class AppModule { }
