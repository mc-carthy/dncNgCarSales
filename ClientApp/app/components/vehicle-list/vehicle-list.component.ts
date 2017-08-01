import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';

@Component({
    templateUrl: 'vehicle-list.component.html',
    styleUrls: [
        'vehicle-list.component.css'
    ]
})

export class VehicleListComponent implements OnInit {
    vehicles: Vehicle[];
    makes: any[];
    models: any[];
    filter: any = {};

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        this.vehicleService.getMakes()
            .subscribe(makes => this.makes = makes);
        this.populateVehicles();
    }

    onFilterChange() {
        this.populateVehicles();
    }

    resetFilter() {
        this.filter = {};
        this.onFilterChange();
    }

    populateModels(makeId) {
        var selectedMake = this.makes.find(m => m.id == makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    private populateVehicles()
    {
        this.vehicleService.getVehicles(this.filter)
            .subscribe(vehicles => this.vehicles = vehicles);
    }
}