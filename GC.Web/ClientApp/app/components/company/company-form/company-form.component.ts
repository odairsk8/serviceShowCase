import { Company } from './../company.model';

import { Component, OnInit, ViewChild } from '@angular/core';
import { CompanyService } from '../company.service';

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

  constructor(private companyService: CompanyService) { }

  ngOnInit() {
  }

  submit() {
    this.companyService.add(this.company).subscribe(r => console.log(r));
  }

}
