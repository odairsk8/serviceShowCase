import { UserMessageService } from './../../shared/user-message.service';
import { IncludedFeatureService } from './../included-feature.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-included-feature-form',
  templateUrl: './included-feature-form.component.html',
  styleUrls: ['./included-feature-form.component.css']
})
export class IncludedFeatureFormComponent implements OnInit {

  providedServiceId: number;
  companyId: number;
  includedFeatureId: number;
  includedFeature: any = {
    id: 0,
    features: []
  };

  constructor(private activatedRoute: ActivatedRoute,
    private service: IncludedFeatureService,
    private userMessageService: UserMessageService) {
    this.verifyRouteParameters();
  }

  verifyRouteParameters() {
    this.activatedRoute.params.subscribe(p => {
      this.providedServiceId = p['providedServiceId'];
      this.companyId = p['companyId'];
      this.includedFeatureId = p['includedFeatureId'];

      if (this.providedServiceId && this.companyId && this.includedFeatureId)
        this.loadForm();
    });
  }

  loadForm() {
    this.service.getById(this.companyId, this.providedServiceId, this.includedFeatureId)
      .subscribe(r => this.includedFeature = r);
  }

  ngOnInit() {
  }

  addItem() {
    this.includedFeature.features.push({});
  }

  removeFeature(featureIndex: number) {
    this.includedFeature.features.splice(featureIndex, 1);
  }

  submit() {

    var message = this.includedFeature.id ? 'Successful updated' : 'Successful added';
    this.includedFeature.providedServiceId = this.providedServiceId;

    let saveAction$ = this.includedFeature.id ? this.service.update(this.companyId, this.providedServiceId, this.includedFeature)
      : this.service.add(this.companyId, this.providedServiceId, this.includedFeature);

    saveAction$
      .subscribe(
      success => {
        this.includedFeature.id = success.id;
        this.userMessageService.success(message);
      }, error => this.userMessageService.error(error));
  }

}
