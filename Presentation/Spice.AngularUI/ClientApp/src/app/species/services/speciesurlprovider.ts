import { Injectable, Output } from '@angular/core';

import { UrlProvider } from '../../services/urlprovider';

@Injectable()
export class SpeciesUrlProvider {
    constructor(private urls: UrlProvider) {
    }
    @Output()
    IndexUrl(): string {
        return this.urls.BaseApiAddress + '/species';
    }

    @Output()
    DetailsUrl(id: string): string {
        return this.urls.BaseApiAddress + '/species/' + id;
    }

    @Output()
    CreateUrl(): string {
        return this.urls.BaseApiAddress + '/species';
    }

    @Output()
    EditUrl(id: string): string {
        return this.urls.BaseApiAddress + '/species/' + id;
    }

    @Output()
    DeleteUrl(id: string): string {
        return this.urls.BaseApiAddress + '/species/' + id;
    }

    @Output()
    SummaryUrl(id: string): string {
        return this.urls.BaseApiAddress + '/species/' + id + '/summary';
    }
}
