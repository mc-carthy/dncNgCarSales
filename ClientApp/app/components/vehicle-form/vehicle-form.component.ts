import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { VehicleService } from './../../services/vehicle.service';
import { ToastyService } from 'ng2-toasty';
import { Observable } from "rxjs/Observable";
import "rxjs/add/Observable/forkJoin";
import * as _ from 'underscore';

import { Vehicle, SaveVehicle } from './../../models/vehicle';
import { NgForm } from "@angular/forms";

@Component({
    selector: 'vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

    makes: any[];
    models: any[];
    features: any[];
    isRegistered: boolean;
    vehicle: SaveVehicle = {
        id: 0,
        makeId: 0,
        modelId: 0,
        isRegistered: false,
        features: [],
        contact: {
            name: '',
            phone: '',
            email: ''
        }
    };

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private vehicleService: VehicleService,
        private toastyService: ToastyService
    ) {
        route.params.subscribe(p => {
            this.vehicle.id = p['id'];
        });
    }

    ngOnInit() {
        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures()
        ];

        if (this.vehicle.id) {
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));
        }

        Observable.forkJoin(sources)
        .subscribe(data => {
            this.makes = data[0];
            this.features = data[1];
            if (this.vehicle.id) {
                this.setVehicle(data[2]);
                this.populateModels();
            }
        }, err => {
            if (err.status == 404) {
                this.router.navigate(['home']);
            }
        });
    }

    private setVehicle(v: Vehicle)
    {
        this.vehicle.id = v.id;
        this.vehicle.makeId = v.make.id;
        this.vehicle.modelId = v.model.id;
        this.vehicle.isRegistered = v.isRegistered;
        this.vehicle.contact = v.contact;
        this.vehicle.features = _.pluck(v.features, 'id');
    }

    onMakeChange() {
        this.populateModels();
        delete this.vehicle.modelId;
    }

    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    onFeatureToggle(featureId, $event)
    {
        if ($event.target.checked) {
            this.vehicle.features.push(featureId);
        }
        else {
            var index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }

    submit(f: NgForm)
    {
        if (this.vehicle.id) {
            this.vehicleService.update(this.vehicle)
                .subscribe(x => {
                    this.toastyService.success({
                        title: 'Success',
                        msg: 'The vehicle has been successfully updated',
                        theme: 'bootstrap',
                        showClose: true,
                        timeout: 5000
                    });
                });
        }
        else {
            this.vehicleService.create(this.vehicle)
                .subscribe(x => {
                    this.toastyService.success({
                        title: 'Success',
                        msg: 'The vehicle has been successfully created',
                        theme: 'bootstrap',
                        showClose: true,
                        timeout: 5000
                    });                
                });
        }
        f.resetForm();
    }

    delete()
    {
        if (confirm("Are you sure you want to delete this vehicle?")) {
            this.vehicleService.delete(this.vehicle.id)
                .subscribe(x => {
                    this.toastyService.success({
                        title: 'Success',
                        msg: 'The vehicle has been successfully deleted',
                        theme: 'bootstrap',
                        showClose: true,
                        timeout: 5000       
                    }),
                    this.router.navigate(['home']);
                });
        }
    }
}
