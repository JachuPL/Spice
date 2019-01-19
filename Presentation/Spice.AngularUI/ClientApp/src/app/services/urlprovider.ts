import { Injectable, Output } from '@angular/core';

@Injectable()
export class UrlProvider {
    @Output() readonly BaseAddress: string = 'https://192.168.0.49';
    @Output() readonly BaseApiAddress: string = this.BaseAddress + '/api';
}
