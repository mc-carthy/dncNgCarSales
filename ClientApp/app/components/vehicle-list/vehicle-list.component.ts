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
    allVehicles: Vehicle[];
    makes: any[];
    models: any[];
    filter: any = {};

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        this.vehicleService.getMakes()
            .subscribe(makes => {
                this.makes = makes;
                console.log(makes);
            });
        this.vehicleService.getVehicles()
            .subscribe(vehicles => this.vehicles = this.allVehicles = vehicles);
    }

    onFilterChange() {
        var vehicles = this.allVehicles;

        if (this.filter.makeId) {
            vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
        }
        if (this.filter.modelId) {
            vehicles = vehicles.filter(v => v.model.id == this.filter.modelId);
        }

        this.vehicles = vehicles;
    }

    resetFilter() {
        this.filter = {};
        this.onFilterChange();
    }

    populateModels(makeId) {
        var selectedMake = this.makes.find(m => m.id == makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }
}