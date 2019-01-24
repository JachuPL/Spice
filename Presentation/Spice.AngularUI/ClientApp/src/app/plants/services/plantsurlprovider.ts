import { Injectable, Output } from '@angular/core';
import { UrlProvider } from '../../services/urlprovider';

@Injectable()
export class PlantsUrlProvider {
    constructor(private urls: UrlProvider) {
    }
    @Output()
    IndexUrl(): string {
        return this.urls.BaseApiAddress + '/plants';
    }

    @Output()
    DetailsUrl(id: string): string {
        return this.urls.BaseApiAddress + '/plants/' + id;
    }

    @Output()
    CreateUrl(): string {
        return this.urls.BaseApiAddress + '/plants';
    }

    @Output()
    EditUrl(id: string): string {
        return this.urls.BaseApiAddress + '/plants/' + id;
    }

    @Output()
    DeleteUrl(id: string): string {
        return this.urls.BaseApiAddress + '/plants/' + id;
    }

    @Output()
    SummaryUrl(id: string): string {
        return this.urls.BaseApiAddress + '/plants/' + id;
    }
}
