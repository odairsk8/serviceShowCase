import { Company } from './company.model';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class CompanyService {

    private readonly API_ENDPOINT: string = "/api/company";

    constructor(private http: Http) { }

    public add(company: Company) {
        return this.http.post(this.API_ENDPOINT, company)
            .map(response => response.json());
    }

    public update(company: Company) {
        return this.http.put(`${this.API_ENDPOINT}/${company.id}`, company)
            .map(r => r.json());
    }

    public delete(id: number) {
        return this.http.delete(this.API_ENDPOINT + '/' + id)
            .map(r => r.json());
    }

    public getById(id: number) {
        return this.http.get(`${this.API_ENDPOINT}/${id}`).map(r => r.json());
    }

    public getProvidedServices(query: any) {
        return this.http.get(`${this.API_ENDPOINT}/${query.companyId}/ProvidedService?`
            + this.toQueryString(query))
            .map(r => r.json());
    }

    public addProvidedService(providedService: any) {
        return this.http.post(`${this.API_ENDPOINT}/${providedService.companyId}/ProvidedService`, providedService)
            .map(r => r.json());
    }

    public updateProvidedService(providedService: any) {
        return this.http.put(`${this.API_ENDPOINT}/${providedService.companyId}/ProvidedService`, providedService)
            .map(r => r.json());
    }

    public getProvidedServiceById(companyId: number, providedServiceId: number) {
        return this.http.get(`${this.API_ENDPOINT}/${companyId}/ProvidedService/${providedServiceId}`)
            .map(r => r.json());
    }

    public deleteProvidedServiceById(companyId: number, providedServiceId: number) {
        return this.http.delete(`${this.API_ENDPOINT}/${companyId}/ProvidedService/${providedServiceId}`)
            .map(r => r.json());
    }

    getByQuery(query: any) {
        return this.http.get(this.API_ENDPOINT + '?' + this.toQueryString(query)).map(r => r.json());
    }

    toQueryString(obj: any) {
        var parts = [];
        for (var property in obj) {
            var value = obj[property];
            if (value != null && value != undefined) {
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
            }
        }
        return parts.join('&');
    }

}