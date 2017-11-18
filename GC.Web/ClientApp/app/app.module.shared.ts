
import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CompanyFormComponent } from './components/company/company-form/company-form.component';
import { CompanyListComponent } from './components/company/company-list/company-list.component';
import { PaginationComponent } from './components/shared/pagination.component';
import { CompanyDetailsComponent } from './components/company/company-details/company-details.component';

import { CompanyService } from './components/company/company.service';
import { PhotoService } from './shared-services/photo.service';
import { UserMessageService } from './components/shared/user-message.service';

import { AppErrorHandler } from './app-error-handler';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CompanyDetailsComponent,
        CompanyFormComponent,
        CompanyListComponent,
        PaginationComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'company', pathMatch: 'full' },
            { path: 'company/details/:id', component: CompanyDetailsComponent },
            { path: 'company/new', component: CompanyFormComponent },
            { path: 'company/edit/:id', component: CompanyFormComponent },
            { path: 'company', component: CompanyListComponent },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        CompanyService, 
        UserMessageService,
        PhotoService
    ]
})
export class AppModuleShared {
}
