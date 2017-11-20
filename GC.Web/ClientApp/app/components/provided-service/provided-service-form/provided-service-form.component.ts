

import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { CompanyService } from './../../company/company.service';
import { UserMessageService } from './../../shared/user-message.service';

@Component({
  selector: 'app-provided-service-form',
  templateUrl: './provided-service-form.component.html',
  styleUrls: ['./provided-service-form.component.css']
})
export class ProvidedServiceFormComponent implements OnInit {

  providedService: any = {};
  companyId: number;

  constructor(private activatedRoute: ActivatedRoute,
    private companyService: CompanyService,
    private userMessageService: UserMessageService) {

    this.checkInitialParameters();
  }

  ngOnInit() {
  }

  private checkInitialParameters() {
    this.activatedRoute.params.subscribe(params => {
      if (params['companyId'])
        this.companyId = +params['companyId'];
        
      if (params['providedServiceId'] && !isNaN(params['providedServiceId'])) {
        this.providedService.id = +params['providedServiceId'];
        this.companyService.getProvidedServiceById(this.companyId, this.providedService.id)
          .subscribe(r => this.loadForm(r));
      }

    })
  }

  private loadForm(data: any) {
    console.log(data);
    
    this.providedService = data;
  }

  submit() {
    this.providedService.companyId = this.companyId;

    var message = this.providedService.id ? 'Successful updated' : 'Successful added';
    let saveAction$ = this.providedService.id ? this.companyService.updateProvidedService(this.providedService)
      : this.companyService.addProvidedService(this.providedService);
    saveAction$
      .subscribe(success => {
        this.providedService.id = success.id;
        this.userMessageService.success(message);
      });
  }

}
