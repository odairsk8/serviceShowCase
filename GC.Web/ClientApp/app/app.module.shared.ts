
import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule, BrowserXhr } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CompanyFormComponent } from './components/company/company-form/company-form.component';
import { CompanyListComponent } from './components/company/company-list/company-list.component';
import { PaginationComponent } from './components/shared/pagination.component';
import { CompanyDetailsComponent } from './components/company/company-details/company-details.component';
import { ProvidedServiceFormComponent } from './components/provided-service/provided-service-form/provided-service-form.component';
import { ProvidedServiceListComponent } from './components/provided-service/provided-service-list/provided-service-list.component';


import { CompanyService } from './components/company/company.service';
import { PhotoService } from './shared-services/photo.service';
import { UserMessageService } from './components/shared/user-message.service';

import { AppErrorHandler } from './app-error-handler';

import { BrowserXhrWithProgress, ProgressService } from './shared-services/progress.service';
import { ProvidedServiceDetailsComponent } from './components/provided-service/provided-service-details/provided-service-details.component';
import { IncludedFeatureFormComponent } from './components/included-feature/included-feature-form/included-feature-form.component';
import { IncludedFeatureService } from './components/included-feature/included-feature.service';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        PaginationComponent,
        CompanyDetailsComponent,
        CompanyFormComponent,
        CompanyListComponent,
        ProvidedServiceListComponent,
        ProvidedServiceFormComponent,
        ProvidedServiceDetailsComponent,
        IncludedFeatureFormComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'company', pathMatch: 'full' },
            { path: 'company', component: CompanyListComponent },
            { path: 'company/details/:id', component: CompanyDetailsComponent },
            { path: 'company/new', component: CompanyFormComponent },
            { path: 'company/edit/:id', component: CompanyFormComponent },
            { path: 'company/:companyId/providedServices', component: ProvidedServiceListComponent },
            { path: 'company/:companyId/providedServices/new', component: ProvidedServiceFormComponent },
            { path: 'company/:companyId/providedServices/edit/:providedServiceId', component: ProvidedServiceFormComponent },
            { path: 'company/:companyId/providedServices/details/:providedServiceId', component: ProvidedServiceDetailsComponent },
            { path: 'company/:companyId/providedService/:providedServiceId/IncludedFeature/new', component: IncludedFeatureFormComponent },
            { path: 'company/:companyId/providedService/:providedServiceId/IncludedFeature/:includedFeatureId', component: IncludedFeatureFormComponent },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        { provide: BrowserXhr, useClass: BrowserXhrWithProgress },
        CompanyService, 
        UserMessageService,
        IncludedFeatureService,
        PhotoService,
        ProgressService
    ]
})
export class AppModuleShared {
}
