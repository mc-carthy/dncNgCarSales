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
    query: any = {};
    columns = [
        {
            title: 'Id',
            key: 'id',
            isSortable: false
        },
        {
            title: 'Contact Name',
            key: 'contactName',
            isSortable: true
        },
        {
            title: 'Make',
            key: 'make',
            isSortable: true
        },
        {
            title: 'Model',
            key: 'model',
            isSortable: true
        },
        { }
    ]

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        this.vehicleService.getMakes()
            .subscribe(makes => this.makes = makes);
        this.populateVehicles();
    }

    onFilterChange() {
        this.populateModels();
        this.populateVehicles();
    }

    resetFilter() {
        this.query = {};
        this.onFilterChange();
    }

    resetModel()
    {
        this.query.modelId = null;
    }

    populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.query.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    sortBy(columnName)
    {
        if (this.query.sortBy == columnName) {
            this.query.isSortAscending = !this.query.isSortAscending;
        } else {
            this.query.sortBy = columnName;
            this.query.isSortAscending = true;
        }

        this.populateVehicles();
    }

    private populateVehicles()
    {
        this.vehicleService.getVehicles(this.query)
            .subscribe(vehicles => this.vehicles = vehicles);
    }
}