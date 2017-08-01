import { Component, OnInit } from '@angular/core';
import { Vehicle } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';

@Component({
    templateUrl: 'vehicle-list.component.html',
    styleUrls: [
        'vehicle-list.component.css'
    ]
})

export class VehicleListComponent implements OnInit {
    vehicles: Vehicle[];

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        this.vehicleService.getVehicles()
            .subscribe(vehicles => this.vehicles = vehicles);
    }
}