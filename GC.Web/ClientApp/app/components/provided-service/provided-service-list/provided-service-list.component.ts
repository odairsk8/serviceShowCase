

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Company } from '../../company/company.model';
import { CompanyService } from './../../company/company.service';


@Component({
  selector: 'app-provided-service-list',
  templateUrl: './provided-service-list.component.html',
  styleUrls: ['./provided-service-list.component.css']
})
export class ProvidedServiceListComponent implements OnInit {

  private readonly PAGE_SIZE = 5;
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'id' },
    { title: 'Name', key: 'name', isSortable: true },
    {}
  ];
  company: Company = new Company();

  constructor(private companyService: CompanyService,
    private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(r => {
      if (r['companyId'])
        this.company.id = +r['companyId'];
    });
  }

  ngOnInit() {
    this.populateList();
  }

  populateList() {
    this.query.companyId = this.company.id;
    this.companyService.getProvidedServices(this.query).subscribe(result => {
      this.queryResult = result;
    });
  }

  onPageChange(pageNumber: any) {
    this.query.page = pageNumber;
    this.populateList();
  }

  sortBy(columnName: string) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateList();
  }

}
