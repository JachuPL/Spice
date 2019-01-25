import { Component, OnInit, Input } from '@angular/core';
import { PlantIndexModel } from '../models/plant.index.model';
import { PlantsService } from '../services/plants.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-plants-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class PlantListComponent implements OnInit {
  @Input('plants') plants: PlantIndexModel[];
  constructor(private plantsService: PlantsService,
    private router: Router) { }

  ngOnInit() {
  }

  onClick(id: string) {
    this.router.navigate(['/plants', id]);
  }
}
