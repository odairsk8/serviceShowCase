


import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastyService } from 'ng2-toasty';

import { CompanyService } from '../company.service';
import { UserMessageService } from './../../shared/user-message.service';
import { Company } from './../company.model';


@Component({
  selector: 'app-company-form',
  templateUrl: './company-form.component.html',
  styleUrls: ['./company-form.component.css']
})
export class CompanyFormComponent implements OnInit {

  company: Company = {
    id: 0,
    name: '',
    foundation: undefined,
    history: ''
  }

  constructor(private companyService: CompanyService,
    private userMessageService: UserMessageService,
    private router: Router,
    private activatedRouter: ActivatedRoute) {
    this.activatedRouter.params.subscribe(r => {
      if (r['id'])
        this.company.id = +r['id'];
    });
  }

  ngOnInit() {
    if(this.company.id)
      this.companyService.getById(this.company.id).subscribe(c => {
        console.log(c);
        this.setCompany(c);
      });
  }

  setCompany(data: any){
    this.company = Object.assign(new Company(), data);
  }

  submit() {
    var message = this.company.id ?  'Successful updated' : 'Successful added';
    let saveAction$ = this.company.id ? this.companyService.update(this.company) : this.companyService.add(this.company);
      saveAction$
      .subscribe(success => {
        this.company.id = success.id;
        this.userMessageService.success(message);
      });
  }

}
