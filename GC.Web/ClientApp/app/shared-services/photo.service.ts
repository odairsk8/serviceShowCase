import { Http } from '@angular/http';
import { Injectable } from '@angular/core';


@Injectable()
export class PhotoService {

    private readonly PHOTO_ENDPOINT = "/api/photo";

    constructor(private http: Http) { }

    uploadCompanyPhoto(companyId: number, photo: any) {
        var formData = new FormData();
        formData.append('file', photo);
        return this.http.post(`${this.PHOTO_ENDPOINT}/company/${companyId}`, formData)
            .map(m => m.json());
    }

    // getPhotos(vehicleId: number) {
    //     return this.http.get(`/api/vehicle/${vehicleId}/photos`)
    //         .map(m => m.json());
    // }


}