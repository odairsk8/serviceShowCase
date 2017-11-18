
import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { CompanyService } from '../company.service';
import { UserMessageService } from '../../shared/user-message.service';
import { Company } from '../company.model';
import { PhotoService } from './../../../shared-services/photo.service';
import { ProgressService } from './../../../shared-services/progress.service';


@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.css']
})
export class CompanyDetailsComponent implements OnInit {

  @ViewChild('fileInput') fileInput: any;
  company: Company = new Company();
  photos: any[] = [];
  progress: any ;

  constructor(private companyService: CompanyService,
    private userMessageService: UserMessageService,
    private progressService: ProgressService,
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

    this.progressService.startTracking()
      .subscribe(
      progress => {

        this.ngZone.run(() => {
          this.progress = progress;
        });
      },
      (error) => { console.log('Error:', error); },
      () => this.progress = null);

    var nativeElement = this.fileInput.nativeElement;
    let file = nativeElement.files[0];
    nativeElement.value = '';
    this.photoService.uploadCompanyPhoto(this.company.id, file)
      .subscribe(photo => {
        this.company.photos.push(photo);
      },
      error => {
        console.log('error: ', error);
        this.userMessageService.error(error);
      });
  }

  delete() {
    this.companyService.delete(this.company.id).subscribe(r => {
      this.userMessageService.error('company deleted');
      this.router.navigate(['/company']);
    });
  }

}
