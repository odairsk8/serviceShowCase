
import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { CompanyService } from '../company.service';
import { UserMessageService } from '../../shared/user-message.service';
import { Company } from '../company.model';
import { PhotoService } from './../../../shared-services/photo.service';


@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.css']
})
export class CompanyDetailsComponent implements OnInit {

  @ViewChild('fileInput') fileInput: any;
  company: Company = new Company();
  photos: any[] = [];

  constructor(private companyService: CompanyService,
    private userMessageService: UserMessageService,
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private photoService: PhotoService,
    private ngZone: NgZone) {
    this.activatedRouter.params.subscribe(r => {
      if (r['id'])
        this.company.id = +r['id'];
    });
  }

  ngOnInit() {
    if (this.company.id)
      this.loadDataFromServer();
  }

  private loadDataFromServer() {
    this.companyService.getById(this.company.id).subscribe(c => {
      this.setCompany(c);
    });
  }
  private setCompany(data: any) {
      this.company = Object.assign(new Company(), data);
  }

  uploadPhoto() {

    var nativeElement = this.fileInput.nativeElement;
    let file = nativeElement.files[0];
    nativeElement.value = '';
    this.photoService.uploadCompanyPhoto(this.company.id, file)
      .subscribe(photo => {
        console.log('photo: ', photo);
        this.photos.push(photo);
        console.log(this.photos);
      },
      error => {
        console.log('error: ', error);
        this.userMessageService.error(error);
      });
  }

  delete(){
    this.companyService.delete(this.company.id).subscribe(r => {
      this.userMessageService.error('company deleted');
      this.router.navigate(['/company']);
    });
  }

}
