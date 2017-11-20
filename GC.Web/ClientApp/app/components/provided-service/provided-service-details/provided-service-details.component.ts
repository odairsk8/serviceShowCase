import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { CompanyService } from '../../company/company.service';
import { UserMessageService } from '../../shared/user-message.service';
import { ProgressService } from '../../../shared-services/progress.service';
import { PhotoService } from '../../../shared-services/photo.service';


@Component({
  selector: 'app-provided-service-details',
  templateUrl: './provided-service-details.component.html',
  styleUrls: ['./provided-service-details.component.css']
})
export class ProvidedServiceDetailsComponent implements OnInit {

  providedService: any = {};
  companyId: number;
  coverProgress: any ;
  @ViewChild("coverFileInput") coverFileInput: any;

  constructor(private activatedRoute: ActivatedRoute,
    private companyService: CompanyService,
    private userMessageService: UserMessageService,
    private router: Router,
    private ngZone: NgZone,
    private photoService: PhotoService,
    private progressService: ProgressService,) {

    this.checkInitialParameters();
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
    this.providedService = data;
  }

  ngOnInit() {
  }

  delete() {
    this.companyService.deleteProvidedServiceById(this.companyId, this.providedService.id).subscribe(r => {
      this.userMessageService.success('Successfuly deleted');
      this.router.navigate(['/company', this.companyId, 'providedServices']);
    });
  }

  uploadCoverPhoto(){
    this.progressService.startTracking()
    .subscribe(
    progress => {

      this.ngZone.run(() => {
        this.coverProgress = progress;
      });
    },
    (error) => { console.log('Error:', error); },
    () => this.coverProgress = null);

    var nativeElement = this.coverFileInput.nativeElement;
    let file = nativeElement.files[0];
    nativeElement.value = '';
    this.photoService.uploadProvidedServiceCoverPhoto(this.companyId, this.providedService.id, file)
      .subscribe(photo => {        
        this.providedService.coverImage = photo;
      },
      error => {
        console.log('error: ', error);
        this.userMessageService.error(error);
      });
  }

  removeCoverPhoto(){
    this.photoService.removeProvidedServiceCoverPhoto(this.companyId, this.providedService.id)
    .subscribe(photo => {
      this.providedService.coverImage = undefined;
    },
    error => {
      console.log('error: ', error);
      this.userMessageService.error(error);
    });
  }

}
