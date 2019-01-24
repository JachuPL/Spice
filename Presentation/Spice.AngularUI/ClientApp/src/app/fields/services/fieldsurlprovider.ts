import { Injectable, Output } from '@angular/core';
import { UrlProvider } from 'src/app/services/urlprovider';

@Injectable()
export class FieldsUrlProvider {
    constructor(private urls: UrlProvider) {
    }
    @Output()
    IndexUrl(): string {
        return this.urls.BaseApiAddress + '/fields';
    }

    @Output()
    DetailsUrl(id: string): string {
        return this.urls.BaseApiAddress + '/fields/' + id;
    }

    @Output()
    CreateUrl(): string {
        return this.urls.BaseApiAddress + '/fields';
    }

    @Output()
    EditUrl(id: string): string {
        return this.urls.BaseApiAddress + '/fields/' + id;
    }

    @Output()
    DeleteUrl(id: string): string {
        return this.urls.BaseApiAddress + '/fields/' + id;
    }
}
