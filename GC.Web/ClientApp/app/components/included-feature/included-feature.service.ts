import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class IncludedFeatureService {

    constructor(private http: Http) { }

    public getById(companyId: number, providedServiceId: number, includedFeatureId: number)
    {
        var url = `/api/Company/${companyId}/ProvidedService/${providedServiceId}/IncludedFeature/${includedFeatureId}`;
        return this.http.get(url).map(r => { return r.json(); });
    }

    public add(companyId: number, providedServiceId: number, includedFeature: any){
        var url = `/api/Company/${companyId}/ProvidedService/${providedServiceId}/IncludedFeature`;
        return this.http.post(url, includedFeature).map(r => { return r.json(); });
    }

    public update(companyId: number, providedServiceId: number, includedFeature: any){
        var url = `/api/Company/${companyId}/ProvidedService/${providedServiceId}/IncludedFeature/${includedFeature.id}`;
        return this.http.put(url, includedFeature).map(r => { return r.json(); });
    }

    public delete(companyId: number, providedServiceId: number, includedFeatureId: any){
        var url = `/api/Company/${companyId}/ProvidedService/${providedServiceId}/IncludedFeature/${includedFeatureId}`;
        return this.http.delete(url).map(r => { return r.json(); });
    }

    public getProvidedServiceFeatures(companyId: number, providedServiceId: number) {
        var url = `/api/Company/${companyId}/ProvidedService/${providedServiceId}/features`;
        return this.http.get(url).map(r => {
            console.log(r);
            return r.json();
        });
    }

}