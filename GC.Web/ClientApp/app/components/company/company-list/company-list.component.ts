
import { Component, OnInit } from '@angular/core';
import { CompanyService } from './../company.service';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent implements OnInit {

  private readonly PAGE_SIZE = 5;

  columns = [
    { title: 'id' },
    { title: 'Name', key: 'name', isSortable: true },
    { title: 'Foundation', key: 'foundation', isSortable: true },
    {}
  ];

  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private service: CompanyService) { }

  ngOnInit() {
    this.populateList();
  }

  populateList() {
    console.log(this.query);
    this.service.getByQuery(this.query).subscribe(result => {
      this.queryResult = result
      console.log(this.queryResult);
    });
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

  onPageChange(pageNumber: any) {
    this.query.page = pageNumber;
    this.populateList();
  }

  resetFilter(){
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateList();
  }

  filter(){
    this.query.page = 1;
    this.populateList();
  }
}
