
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CompanyFormComponent } from './components/company/company-form/company-form.component';
import { CompanyService } from './components/company/company.service';
import { CompanyListComponent } from './components/company/company-list/company-list.component';
import { PaginationComponent } from './components/shared/pagination.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CompanyFormComponent,
        CompanyListComponent,
        PaginationComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'company/new', pathMatch: 'full' },
            { path: 'company/new', component: CompanyFormComponent },
            { path: 'company', component: CompanyListComponent },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        CompanyService
    ]
})
export class AppModuleShared {
}
